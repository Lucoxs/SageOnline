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

        builder.WebHost.UseUrls("https://localhost:7001");

        builder.Services.AddSingleton<IConfiguration>(builder.Configuration);

        builder.Services.AddControllersWithViews();
        builder.Services.AddSwaggerGen();

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
            /*SeedData();*/
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        /*SeedData();*/

        app.UseRouting();
        app.UseIdentityServer();
        app.UseAuthorization();
        app.MapRazorPages().RequireAuthorization();
        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();

        /*void SeedData()
        {
            using var scope = app.Services.CreateScope();
            var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializerService>();
            dbInitializer.Initialize();
        }*/
    }
}