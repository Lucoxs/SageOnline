using API.Identity.Context;
using API.Identity.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace API.Identity.DAO
{
    public class CompanyDAO
    {
        public static async Task<List<Company>> GetCompanies(AppDbContext context)
        {
            return await context.Companies.Include(x => x.Users).ToListAsync();
        }

        public static async Task<Company?> GetCompany(AppDbContext context, int id)
        {
            if (context.Companies == null)
                return null;

            return await context.Companies.SingleOrDefaultAsync(y => y.Id == id);
        }

        public static async Task<bool> ExistCompanyByName(AppDbContext context, string name)
        {
            return await context.Companies.AnyAsync(y => y.Name == name);
        }

        public static async Task RemoveCompany(AppDbContext context, Company company)
        {
            context.Companies.Remove(company);
            await context.SaveChangesAsync();
        }
    }
}
