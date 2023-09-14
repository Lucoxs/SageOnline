using API.Identity.Context;
using API.Identity.DAO;
using API.Identity.DTO.Company;
using API.Identity.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace API.Identity.Services
{
    public class CompanyService
    {
        private readonly AppDbContext _dbContext;

        public CompanyService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Company>> GetCompanies()
        {
            return await CompanyDAO.GetCompanies(_dbContext);
        }

        public async Task<Company?> GetCompany(Guid id)
        {
            return await CompanyDAO.GetCompany(_dbContext, id);
        }

        public async Task<bool> ExistCompanyByName(string name)
        {
            return await CompanyDAO.ExistCompanyByName(_dbContext, name);
        }

        public async Task RemoveCompany(Company company)
        {
            await CompanyDAO.RemoveCompany(_dbContext, company);
        }

        public async Task<Company> RegisterAsync(CompanyNewDTO companyNewDto)
        {
            Company company = new()
            {
                Name = companyNewDto.Name,
                Activity = companyNewDto.Activity,
                LegalStatus = companyNewDto.LegalStatus,
                Capital = companyNewDto.Capital,
                Address = companyNewDto.Address,
                Complement = companyNewDto.Complement,
                Zip = companyNewDto.Zip,
                City = companyNewDto.City,
                Region = companyNewDto.Region,
                Country = companyNewDto.Country,
                Siret = companyNewDto.Siret,
                VatIdentifier = companyNewDto.VatIdentifier,
                NafCode = companyNewDto.NafCode,
                Website = companyNewDto.Website,
                Phone = companyNewDto.Phone,
                Email = companyNewDto.Email,
                MaxUsers = companyNewDto.MaxUsers,
            };

            return await RegisterCompanyAsync(company);
        }

        public async Task<Company> RegisterCompanyAsync(Company company)
        {
            await _dbContext.Companies.AddAsync(company);
            await _dbContext.SaveChangesAsync();

            return company;
        }

        public async Task UpdateAsync(Company company, CompanyDTO companyDto)
        {
            company.Activity = companyDto.Activity;
            company.LegalStatus = companyDto.LegalStatus;
            company.Capital = companyDto.Capital;
            company.Address = companyDto.Address;
            company.Complement = companyDto.Complement;
            company.Zip = companyDto.Zip;
            company.City = companyDto.City;
            company.Region = companyDto.Region;
            company.Country = companyDto.Country;
            company.Siret = companyDto.Siret;
            company.VatIdentifier = companyDto.VatIdentifier;
            company.NafCode = companyDto.NafCode;
            company.Website = companyDto.Website;
            company.Phone = companyDto.Phone;
            company.Email = companyDto.Email;
            company.MaxUsers = companyDto.MaxUsers;

            await _dbContext.SaveChangesAsync();
        }
    }
}
