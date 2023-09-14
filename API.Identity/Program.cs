using API.Identity.Context;
using API.Identity.Interfaces;
using API.Identity.Models;
using API.Identity.Services;
using Duende.IdentityServer.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddSingleton<IConfiguration>(builder.Configuration);

        builder.Services.AddControllersWithViews();
        builder.Services.AddSwaggerGen();

        builder.Services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConfig"));
        });

        /*Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .CreateLogger();

        builder.Services.AddSingleton(Log.Logger);*/

        /*builder.Services.AddSingleton<ICorsPolicyService>((container) => {
            var logger = container.GetRequiredService<ILogger<DefaultCorsPolicyService>>();
            return new DefaultCorsPolicyService(logger)
            {
                AllowedOrigins = { "http://localhost:3000/" }
            };
        });*/

        /*builder.Services.AddSingleton<ICorsPolicyService>((container) =>
        {
            var logger = container.GetRequiredService<ILogger<DefaultCorsPolicyService>>();
            return new DefaultCorsPolicyService(logger)
            {
                AllowAll = true
            };
        });
*/
        /*builder.Services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy",
                builder => builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
        });*/

        /*builder.Services.AddSingleton<ICorsPolicyService>((container) => {
            var logger = container.GetRequiredService<ILogger<DefaultCorsPolicyService>>();
            return new DefaultCorsPolicyService(logger)
            {
                AllowAll = true
            };
        });*/

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

        /*builder.Services.AddScoped<ICorsPolicyService, CorsPolicyService>();*/
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

        /*app.UseCors("CorsPolicy");*/

        app.Run();

        void SeedData()
        {
            using var scope = app.Services.CreateScope();
            var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializerService>();
            dbInitializer.Initialize();
        }
    }
}