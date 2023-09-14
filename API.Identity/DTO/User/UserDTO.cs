using Newtonsoft.Json;

namespace API.Identity.DTO.User
{
    public class UserDTO
    {
        [JsonProperty("id")]
        public string? Id { get; set; }

        [JsonProperty("firstname")]
        public string? FirstName { get; set; }

        [JsonProperty("lastname")]
        public string? LastName { get; set; }

        [JsonProperty("email")]
        public string? Email { get; set; }

        [JsonProperty("phone")]
        public string? PhoneNumber { get; set; }

        [JsonProperty("role")]
        public string? Role { get; set; }

        [JsonProperty("company_id")]
        public Guid? CompanyId { get; set; }

        public UserDTO()
        {
            
        }

        public UserDTO(Models.User user, string? role)
        {
            Id = user.Id;
            FirstName = user.FirstName;
            LastName = user.LastName;
            Email = user.Email;
            PhoneNumber = user.PhoneNumber;
            Role = role;
            CompanyId = user.Company.Id;
        }
    }
}
