using API.Identity.Context;
using API.Identity.Interfaces;
using API.Identity.Models;
using Duende.IdentityServer.Validation;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Protocols;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.Security.Claims;
using static Duende.IdentityServer.Models.IdentityResources;

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
            //_dbContext.Database.EnsureCreated();
            //_dbContext.Database.Migrate();
/*
            if (_roleManager.FindByNameAsync(Config.SuperAdmin).GetAwaiter().GetResult() == null)
            {
                _roleManager.CreateAsync(new IdentityRole(Config.SuperAdmin)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(Config.Admin)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(Config.User)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(Config.OfflineAccess)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(Config.OpenId)).GetAwaiter().GetResult();
            }

            Company company = new()
            {
                Name = "Sage",
                Activity = "Softwares",
                LegalStatus = "SAS",
                Capital = 1_800_000,
                Address = "94 rue Saint Lazare",
                Complement = "Bat. D",
                Zip = "75009",
                City = "Paris",
                Region = "Île-de-France",
                Country = "France",
                Siret = "12346789",
                VatIdentifier = "123456789",
                NafCode = "123456789",
                Website = "https://www.sage.com/",
                Phone = "0102030405",
                Email = "strohllucas93160@gmail.com",
                MaxUsers = 1000
            };

            if (!_dbContext.Companies.Any(x => x.Name == company.Name))
            {
                _dbContext.Companies.Add(company);
                _dbContext.SaveChanges();
            }
            else
                company = _dbContext.Companies.First(x => x.Name == company.Name);

            User superAdminUser = new()
            {
                Email = "strohllucas93160@gmail.com",
                NormalizedEmail = "strohllucas93160@gmail.com".ToUpper(),
                LastName = "Strohl",
                FirstName = "Lucas",
                UserName = "Strohl Lucas".Replace(" ", "_"),
                NormalizedUserName = "Strohl Lucas".Replace(" ", "_").ToUpper(),
                EmailConfirmed = true,
                PhoneNumber = "0102030405",
                Company = company
            };

            if (_userManager.Users.FirstOrDefault(x => x.Email == superAdminUser.Email) == null)
            {
                _userManager.CreateAsync(superAdminUser*//*, "Admin123*"*//*).GetAwaiter().GetResult();
                _userManager.AddToRoleAsync(superAdminUser, Config.SuperAdmin).GetAwaiter().GetResult();
                _userManager.AddToRoleAsync(superAdminUser, Config.Admin).GetAwaiter().GetResult();
                _userManager.AddToRoleAsync(superAdminUser, Config.User).GetAwaiter().GetResult();
                _userManager.AddToRoleAsync(superAdminUser, Config.OfflineAccess).GetAwaiter().GetResult();
                _userManager.AddToRoleAsync(superAdminUser, Config.OpenId).GetAwaiter().GetResult();

               *//* _userManager.AddClaimsAsync(superAdminUser, new Claim[]
                {
                     new Claim(JwtClaimTypes.Name, superAdminUser.UserName),
                     new Claim(JwtClaimTypes.Role, Config.SuperAdmin)
                }).GetAwaiter().GetResult();*//*
            }

            User superAdmin2 = new()
            {
                Email = "strohl@gmail.com",
                NormalizedEmail = "strohl@gmail.com".ToUpper(),
                LastName = "Strohl",
                FirstName = "Luc",
                UserName = "Strohl".Replace(" ", "_"),
                NormalizedUserName = "Strohl".Replace(" ", "_").ToUpper(),
                EmailConfirmed = true,
                PhoneNumber = "0102030405",
                Company = company
            };

            if (_userManager.Users.FirstOrDefault(x => x.Email == superAdmin2.Email) == null)
            {
                var t = _userManager.CreateAsync(superAdmin2*//*, "Admin123*"*//*).GetAwaiter().GetResult();
                _userManager.AddToRoleAsync(superAdmin2, Config.User).GetAwaiter().GetResult();
                _userManager.AddToRoleAsync(superAdmin2, Config.OfflineAccess).GetAwaiter().GetResult();
                _userManager.AddToRoleAsync(superAdmin2, Config.OpenId).GetAwaiter().GetResult();

                *//*_userManager.AddClaimsAsync(superAdmin2, new Claim[]
                {
                     new Claim(JwtClaimTypes.Name, superAdmin2.UserName),
                     new Claim(JwtClaimTypes.Role, Config.SuperAdmin)
                }).GetAwaiter().GetResult();*//*
            }

            User adminUser = new()
            {
                Email = "strohllucas@gmail.com",
                NormalizedEmail = "strohllucas@gmail.com".ToUpper(),
                LastName = "Strohl",
                FirstName = "Lucas",
                UserName = "Strohl lucoxs".Replace(" ", "_"),
                NormalizedUserName = "Strohl Lucas".Replace(" ", "_").ToUpper(),
                EmailConfirmed = true,
                PhoneNumber = "0102030405",
                Company = company
            };

            if (_userManager.Users.FirstOrDefault(x => x.Email == adminUser.Email) == null)
            {
                var t = _userManager.CreateAsync(adminUser*//*, "Admin123*"*//*).GetAwaiter().GetResult();
                _userManager.AddToRoleAsync(adminUser, Config.Admin).GetAwaiter().GetResult();
                _userManager.AddToRoleAsync(adminUser, Config.User).GetAwaiter().GetResult();
                _userManager.AddToRoleAsync(adminUser, Config.OfflineAccess).GetAwaiter().GetResult();
                _userManager.AddToRoleAsync(adminUser, Config.OpenId).GetAwaiter().GetResult();

                *//*_userManager.AddClaimsAsync(adminUser, new Claim[]
                {
                     new Claim(JwtClaimTypes.Name, adminUser.UserName),
                     new Claim(JwtClaimTypes.Role, Config.SuperAdmin)
                }).GetAwaiter().GetResult();*//*
            }*/
        }
    }
}
