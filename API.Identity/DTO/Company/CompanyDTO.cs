using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace API.Identity.DTO.Company
{
    public class CompanyDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Activity { get; set; }
        public string? LegalStatus { get; set; }
        public double? Capital { get; set; }
        public string? Address { get; set; }
        public string? Complement { get; set; }
        public string? Zip { get; set; }
        public string? City { get; set; }
        public string? Region { get; set; }
        public string? Country { get; set; }
        public string? Siret { get; set; }
        public string? VatIdentifier { get; set; }
        public string? NafCode { get; set; }
        public string? Website { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public int MaxUsers { get; set; }

        public CompanyDTO()
        {
            
        }

        public CompanyDTO(Models.Company company)
        {
            Id = company.Id;
            Name = company.Name;
            Activity = company.Activity;
            LegalStatus = company.LegalStatus;
            Capital = company.Capital;
            Address = company.Address;
            Complement = company.Complement;
            Zip = company.Zip;
            City = company.City;
            Region = company.Region;
            Country = company.Country;
            Siret = company.Siret;
            VatIdentifier = company.VatIdentifier;
            NafCode = company.NafCode;
            Website = company.Website;
            Phone = company.Phone;
            Email = company.Email;
            MaxUsers = company.MaxUsers;
        }
    }
}
