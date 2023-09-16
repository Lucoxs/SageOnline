using API.Gateway.Models;
using Newtonsoft.Json;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Request.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var aut = builder.Configuration["Apps:Authority"];/*
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        builder => builder.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());
});*/

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.Authority = builder.Configuration["Apps:Authority"];
        options.Audience = "WebApplication";
        options.TokenValidationParameters = new()
        {
            ValidateAudience = false,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero,
            ValidTypes = new[] { "at+jwt" }
        };
    });

builder.Services.AddOcelot();

var app = builder.Build();

app.UseCors(x => x
    .AllowAnyMethod()
    .AllowAnyHeader()
    .AllowCredentials()
    .SetIsOriginAllowed(origin => true));

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

var configuration = new OcelotPipelineConfiguration
{
    PreAuthenticationMiddleware = async (context, next) =>
    {
        await PreAuthenticationMiddleware(context, next);
    }
};

await app.UseOcelot(configuration);

app.Run();

async Task PreAuthenticationMiddleware(HttpContext context, Func<Task> next)
{
    string? token = context.Request.Headers["Authorization"].ToString()?.Replace("Bearer ", ""); 
    if (string.IsNullOrEmpty(token))
    {
        await next.Invoke();
        return;
    }

    using HttpClient httpClient = new();
    using HttpRequestMessage message = new(HttpMethod.Get, $"{builder.Configuration["Apps:Authority"]}api/v1/auth/userinfo?token={token}");
    var response = await httpClient.SendAsync(message);
    var responseString = await response.Content.ReadAsStringAsync();

    if (!response.IsSuccessStatusCode)
    {
        context.Items.SetError(new UnauthenticatedError(responseString));
        return;
    }

    var tokenClaim = JsonConvert.DeserializeObject<TokenClaim>(responseString);
    if (tokenClaim == null)
    {
        context.Items.SetError(new UnauthenticatedError("Empty claims"));
        return;
    }

    DownstreamRequest downstreamRequest = context.Items.DownstreamRequest();
    downstreamRequest.Headers.Add("user_id", tokenClaim.UserId);
    downstreamRequest.Headers.Add("company_id", tokenClaim.CompanyId);

    await next.Invoke();
}