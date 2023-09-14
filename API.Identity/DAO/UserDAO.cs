using API.Identity.Context;
using API.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Identity.DAO
{
    public class UserDAO
    {
        public static async Task<List<User>> GetUsers(AppDbContext dbContext, Company company)
        {
            return await dbContext.Users.Where(x => x.Company == company).ToListAsync();
        }

        public static async Task<User?> GetUser(AppDbContext dbContext, Company company, string id)
        {
            return await dbContext.Users
                .Where(x => x.Company == company)
                .SingleOrDefaultAsync(y => y.Id == id);
        }

        public static async Task<bool> ExistUserByEmail(AppDbContext dbContext, Company company, string email)
        {
            return await dbContext.Users
                .Where(x => x.Company == company)
                .AnyAsync(y => y.Email == email);
        }

        public static async Task<User?> GetUserByEmail(AppDbContext dbContext, Company company, string email)
        {
            return await dbContext.Users
                .Where(x => x.Company == company)
                .SingleOrDefaultAsync(y => y.Email == email);
        }

        public static async Task<IdentityResult> Update(UserManager<User> userManager, User user)
        {
            return await userManager.UpdateAsync(user);
        }

        public static async Task<IdentityResult> Create(UserManager<User> userManager, User user)
        {
            return await userManager.CreateAsync(user);
        }

        public static async Task<IdentityResult> Remove(UserManager<User> userManager, User user)
        {
            return await userManager.DeleteAsync(user);
        }
    }
}
