using API.Identity.Models;
using Newtonsoft.Json;

namespace API.Identity.DTO.User
{
    public class UserCompanyDTO
    {
        public Guid Id { get; set; }

        [JsonProperty(nameof(Name))]
        public string Name { get; set; } = default!;

        [JsonProperty(nameof(Activity))]
        public string? Activity { get; set; }

        [JsonProperty(nameof(LegalStatus))]
        public string? LegalStatus { get; set; }

        [JsonProperty(nameof(Capital))]
        public double? Capital { get; set; }

        [JsonProperty(nameof(Address))]
        public string? Address { get; set; }

        [JsonProperty(nameof(Complement))]
        public string? Complement { get; set; }

        [JsonProperty(nameof(Zip))]
        public string? Zip { get; set; }

        [JsonProperty(nameof(City))]
        public string? City { get; set; }

        [JsonProperty(nameof(Region))]
        public string? Region { get; set; }

        [JsonProperty(nameof(Country))]
        public string? Country { get; set; }

        [JsonProperty(nameof(Siret))]
        public string? Siret { get; set; }

        [JsonProperty(nameof(VatIdentifier))]
        public string? VatIdentifier { get; set; }

        [JsonProperty(nameof(NafCode))]
        public string? NafCode { get; set; }

        [JsonProperty(nameof(Website))]
        public string? Website { get; set; }

        [JsonProperty(nameof(Phone))]
        public string? Phone { get; set; }

        [JsonProperty(nameof(Email))]
        public string? Email { get; set; }

        [JsonProperty(nameof(MaxUsers))]
        public int MaxUsers { get; set; }

        public UserCompanyDTO(Models.Company company)
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
