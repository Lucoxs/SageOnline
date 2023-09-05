using API.Identity.Context;
using API.Identity.Interfaces;
using API.Identity.Models;
using Duende.IdentityServer.Validation;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace API.Identity.Services
{
    public class DbInitializerService : IDbInitializerService
    {
        private readonly AppDbContext _dbContext;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DbInitializerService(
            AppDbContext dbContext, 
            UserManager<User> userManager, 
            RoleManager<IdentityRole> roleManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public void Initialize()
        {
            _dbContext.Database.EnsureCreated();
            //_dbContext.Database.Migrate();

            if (_roleManager.FindByNameAsync(Config.SuperAdmin).GetAwaiter().GetResult() != null)
                return;

            _roleManager.CreateAsync(new IdentityRole(Config.SuperAdmin)).GetAwaiter().GetResult();
            _roleManager.CreateAsync(new IdentityRole(Config.Admin)).GetAwaiter().GetResult();
            _roleManager.CreateAsync(new IdentityRole(Config.User)).GetAwaiter().GetResult();

            User superAdminUser = new()
            {
                UserName = Config.SuperAdmin,
                Email = $"{Config.SuperAdmin}@gmail.com",
                EmailConfirmed = true,
                PhoneNumber = "0102030405",
                Name = "Strohl Lucas"
            };

            _userManager.CreateAsync(superAdminUser, "Admin123*").GetAwaiter().GetResult();
            _userManager.AddToRoleAsync(superAdminUser, Config.SuperAdmin).GetAwaiter().GetResult();

            var claims = _userManager.AddClaimsAsync(superAdminUser, new Claim[]
            {
                new Claim(JwtClaimTypes.Name, superAdminUser.Name),
                new Claim(JwtClaimTypes.Role, Config.SuperAdmin)
            }).GetAwaiter().GetResult();
        }
    }
}
