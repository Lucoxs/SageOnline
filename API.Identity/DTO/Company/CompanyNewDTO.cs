namespace API.Identity.DTO.Company
{
    public class CompanyNewDTO
    {
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
    }
}
