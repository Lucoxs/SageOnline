using API.Identity.Context;
using API.Identity.Interfaces;
using API.Identity.Models;
using API.Identity.Services;
using Duende.IdentityServer.Services;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Net;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.Console()
            .CreateLogger();

        builder.Services.AddSingleton<IConfiguration>(builder.Configuration);

        builder.Services.AddControllersWithViews();
        builder.Services.AddSwaggerGen();

        Log.Debug("IdentityConfig");
        var server = builder.Configuration.GetValue<string>("DB_SERVER");
        var port = builder.Configuration.GetValue<string>("DB_PORT");
        var database = builder.Configuration.GetValue<string>("DB_DATABASE");
        var user = builder.Configuration.GetValue<string>("DB_USER");
        var password = builder.Configuration.GetValue<string>("DB_PASS");
        var connectionString = $"Server={server},{port};User={user};Password={password};Database={database};Encrypt=False";

        if (builder.Environment.IsDevelopment())
            connectionString = "Server=(localdb)\\mssqllocaldb;Database=IdentityConfig;Trusted_Connection=True;";

        builder.Services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });
        Log.Debug(connectionString);

        builder.Services.AddDataProtection()
            .PersistKeysToDbContext<AppDbContext>();

        builder.Services.AddIdentity<User, IdentityRole>()
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

        var identityBuilder = builder.Services
            .AddIdentityServer(options =>
            {
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;
                options.EmitStaticAudienceClaim = true;
            })
            .AddInMemoryIdentityResources(Config.IdentityResources)
            .AddInMemoryApiScopes(Config.ApiScopes)
            .AddInMemoryClients(Config.Clients)
            .AddAspNetIdentity<User>()
            .AddCorsPolicyService<CorsPolicyService>();

        builder.Services.AddScoped<IDbInitializerService, DbInitializerService>();
        builder.Services.AddScoped<IProfileService, ProfileService>();
        builder.Services.AddScoped<IAuthService, AuthService>();
        builder.Services.AddScoped<CompanyService>();
        builder.Services.AddScoped<UserService>();

        identityBuilder
            .AddDeveloperSigningCredential();

        builder.Services.AddRazorPages();

        Log.Debug("Build");
        var app = builder.Build();
        
        app.UseCors(x => x
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials()
            .SetIsOriginAllowed(origin => true));

        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }
        else
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        SeedData();

        app.UseRouting();
        app.UseIdentityServer();
        app.UseAuthorization();
        app.MapRazorPages().RequireAuthorization();
        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();
        Log.Debug("Run");

        void SeedData()
        {
            using var scope = app.Services.CreateScope();
            var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializerService>();
            dbInitializer.Initialize();
        }
    }
}