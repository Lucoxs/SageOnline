using API.Identity.Models;
using Newtonsoft.Json;

namespace API.Identity.DTO.User
{
    public class UserNewDTO
    {
        [JsonProperty("Firstname")]
        public string FirstName { get; set; }

        [JsonProperty("Lastname")]
        public string LastName { get; set; }

        [JsonProperty("Email")]
        public string Email { get; set; }

        [JsonProperty("phone")]
        public string? PhoneNumber { get; set; }

        [JsonProperty("Role")]
        public string? Role { get; set; }

        [JsonProperty("company_id")]
        public Guid CompanyId { get; set; }
    }
}
