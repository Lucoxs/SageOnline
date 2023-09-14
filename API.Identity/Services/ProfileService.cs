using API.Identity.Context;
using API.Identity.Models;
using Duende.IdentityServer.Extensions;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace API.Identity.Services
{
    public class ProfileService : IProfileService
    {
        private readonly IUserClaimsPrincipalFactory<User> _userClaimsPrincipalFactory;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly AppDbContext _dbContext;

        public ProfileService(
            UserManager<User> userManager, 
            RoleManager<IdentityRole> roleManager, 
            IUserClaimsPrincipalFactory<User> userClaimsPrincipalFactory,
            AppDbContext dbContext)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _userClaimsPrincipalFactory = userClaimsPrincipalFactory;
            _dbContext = dbContext;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            string sub = context.Subject.GetSubjectId();
            User? user = await _userManager.FindByIdAsync(sub);
            ClaimsPrincipal userClaims = await _userClaimsPrincipalFactory.CreateAsync(user);

            List<Claim> claims = userClaims.Claims.ToList();
            claims = claims.Where(x => context.RequestedClaimTypes.Contains(x.Type)).ToList();
            //claims.Add(new("usid", user.Id));
            claims.Add(new("coid", _dbContext.Companies.Single(x => x.Users.Any(y => y.Id == user.Id)).Id.ToString()));

            /*if (_userManager.SupportsUserRole)
            {
                var roles = await _userManager.GetRolesAsync(user);
                foreach (var rolename in roles)
                {
                    claims.Add(new(JwtClaimTypes.Role, rolename));
                    if (_userManager.SupportsUserClaim)
                    {
                        var role = await _roleManager.FindByNameAsync(rolename);
                        if (role != null)
                        {
                            claims.AddRange(await _roleManager.GetClaimsAsync(role));
                        }
                    }
                }
            }*/

            context.IssuedClaims = claims;
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            string sub = context.Subject.GetSubjectId();
            User? user = await _userManager.FindByIdAsync(sub);
            context.IsActive = user != null;
        }
    }
}
