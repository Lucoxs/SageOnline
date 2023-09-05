using API.IdentityServer.Context;
using API.IdentityServer.DTO;
using API.IdentityServer.Interfaces;
using API.IdentityServer.Models;
using Microsoft.AspNetCore.Identity;

namespace API.IdentityServer.Services
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public AuthService(
            AppDbContext context,
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager,
            IJwtTokenGenerator jwtTokenGenerator)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<UserResponseDTO> Login(UserLoginDTO userLogin)
        {
            var user = _context.Users.FirstOrDefault(x => x.Email != null && x.Email.ToLower() == userLogin.Email.ToLower())
                ?? throw new Exception($"The email {userLogin.Email} doesn't exist!");

            if (!await _userManager.CheckPasswordAsync(user, userLogin.Password))
                throw new Exception("Invalid password!");

            return new()
            {
                Token = _jwtTokenGenerator.GenerateToken(user)
            };
        }

        public async Task<UserDTO> Register(UserRegisterDTO userRegisterDTO)
        {
            User user = new()
            {
                UserName = userRegisterDTO.Email,
                Email = userRegisterDTO.Email,
                NormalizedEmail = userRegisterDTO.Email.ToUpper()
            };
            
            var result = await _userManager.CreateAsync(user, userRegisterDTO.Password);
            if (result.Succeeded)
            {
                User persistantUser = _context.Users.First(x => x.UserName == userRegisterDTO.Email);
                return new()
                {
                    Email = persistantUser.Email
                };
            }

            throw new Exception(result.Errors.FirstOrDefault()?.Description);
        }
    }
}
