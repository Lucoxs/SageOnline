using API.Identity.Context;
using API.Identity.DAO;
using API.Identity.DTO.User;
using API.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using static Duende.IdentityServer.Models.IdentityResources;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace API.Identity.Services
{
    public class UserService
    {
        private readonly AppDbContext _dbContext;
        private readonly UserManager<User> _userManager;

        public UserService(AppDbContext dbContext, UserManager<User> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }
        public async Task<List<User>> GetUsers(Company company)
        {
            return await UserDAO.GetUsers(_dbContext, company);
        }

        public async Task<User?> GetUser(Company company, string id)
        {
            return await UserDAO.GetUser(_dbContext, company, id);
        }

        public async Task<bool> ExistUserByEmail(Company company, string email)
        {
            return await UserDAO.ExistUserByEmail(_dbContext, company, email);
        }

        public async Task<User?> GetUserByEmail(Company company, string email)
        {
            return await UserDAO.GetUserByEmail(_dbContext, company, email);
        }

        public async Task<IdentityResult> UpdateUser(User user, UserDTO userDTO)
        {
            user.FirstName = userDTO.FirstName;
            user.LastName = userDTO.LastName;
            user.PhoneNumber = userDTO.PhoneNumber;

            //TODO : manage roles
            /*user.Role = userDTO.Role;*/

            return await UserDAO.Update(_userManager, user);
        }

        public async Task<IdentityResult> CreateUser(User user)
        {
            return await UserDAO.Create(_userManager, user);
        }

        public async Task<IdentityResult> Remove(User user)
        {
            return await UserDAO.Remove(_userManager, user);
        }
    }
}
