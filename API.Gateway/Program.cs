using API.Gateway;
using API.Gateway.Models;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Logging;
using Newtonsoft.Json;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Request.Middleware;
using Serilog;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.WebHost.UseUrls("https://localhost:7000");

var aut = builder.Configuration["Apps:Authority"];

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .CreateLogger();

IdentityModelEventSource.ShowPII = true;

var DB_SERVER = builder.Configuration.GetValue<string>("DB_SERVER");
var DB_PORT = builder.Configuration.GetValue<string>("DB_PORT");
var DB_DATABASE = builder.Configuration.GetValue<string>("DB_DATABASE");
var DB_USER = builder.Configuration.GetValue<string>("DB_USER");
var DB_PASS = builder.Configuration.GetValue<string>("DB_PASS");

string connectionString = $"Server={DB_SERVER},{DB_PORT};User={DB_USER};Password={DB_PASS};Database={DB_DATABASE};Encrypt=False";
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(connectionString);
});

builder.Services.AddDataProtection()
    .PersistKeysToDbContext<AppDbContext>();

ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;

Log.Debug("auth");

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.RequireHttpsMetadata = false;
        options.Authority = builder.Configuration["Apps:Authority"];
        options.Audience = "WebApplication";
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new()
        {
            ValidateAudience = false,
            ValidateIssuer = false,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero,
            ValidTypes = new[] { "at+jwt" }
        };
    });

Log.Debug("ocelot");
builder.Services.AddOcelot();

Log.Debug("Build");
var app = builder.Build();

Log.Debug("UseCors");
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

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

Log.Debug("Middleware");
var configuration = new OcelotPipelineConfiguration
{
    PreAuthenticationMiddleware = async (context, next) =>
    {
        await PreAuthenticationMiddleware(context, next);
    }
};

await app.UseOcelot(configuration);

Log.Debug("before run");
app.Run();
Log.Debug("after run");

async Task PreAuthenticationMiddleware(HttpContext context, Func<Task> next)
{
    string? token = context.Request.Headers["Authorization"].ToString()?.Replace("Bearer ", ""); 
    if (string.IsNullOrEmpty(token))
    {
        await next.Invoke();
        return;
    }

    using HttpClient httpClient = new();
    using HttpRequestMessage messageAuth = new(HttpMethod.Get, $"{builder.Configuration["Apps:Authority"]}api/v1/auth/userinfo?token={token}");
    var responseAuth = await httpClient.SendAsync(messageAuth);
    var responseString = await responseAuth.Content.ReadAsStringAsync();

    if (!responseAuth.IsSuccessStatusCode)
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

    using HttpRequestMessage messageUser = new(HttpMethod.Get, $"{builder.Configuration["Apps:Authority"]}api/v1/companies/{tokenClaim.CompanyId}/users/{tokenClaim.UserId}");
    var responseUser = await httpClient.SendAsync(messageUser);
    if (!responseUser.IsSuccessStatusCode)
    {
        context.Items.SetError(new UnauthenticatedError("company or user doesn't exist"));
        return;
    }

    DownstreamRequest downstreamRequest = context.Items.DownstreamRequest();
    downstreamRequest.Headers.Add("user_id", tokenClaim.UserId);
    downstreamRequest.Headers.Add("company_id", tokenClaim.CompanyId);

    await next.Invoke();
}