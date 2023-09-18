using API.Documents.Context;
using API.Documents.Services;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .CreateLogger();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

Log.Debug("EncryptionKey");
EncryptionService.Key = builder.Configuration.GetValue<string>("EncryptionKey");
Log.Debug(builder.Configuration.GetValue<string>("EncryptionKey") ?? "error");


Log.Debug("SageOnlineDocuments");
var server = builder.Configuration.GetValue<string>("DB_SERVER");
var port = builder.Configuration.GetValue<string>("DB_PORT");
var database = builder.Configuration.GetValue<string>("DB_DATABASE");
var user = builder.Configuration.GetValue<string>("DB_USER");
var password = builder.Configuration.GetValue<string>("DB_PASS");
var connectionString = $"Server={server},{port};User={user};Password={password};Database={database};";
builder.Services.AddDbContext<AppDbContext>(options =>
{
    /*options.UseSqlServer(builder.Configuration["ConnectionString"]);*/
    options.UseSqlServer(connectionString);
});
Log.Debug(connectionString);

builder.Services.AddScoped<DocumentService>();

Log.Debug("Build");
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
Log.Debug("Run");
