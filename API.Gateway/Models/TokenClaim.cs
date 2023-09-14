using Newtonsoft.Json;

namespace API.Gateway.Models
{
    public class TokenClaim
    {
        [JsonProperty("sub")]
        public string UserId { get; set; }

        [JsonProperty("coid")]
        public string CompanyId { get; set; }
    }
}
