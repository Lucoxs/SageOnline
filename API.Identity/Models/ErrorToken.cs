using Newtonsoft.Json;

namespace API.Identity.Models
{
    public class ErrorToken
    {
        [JsonProperty("error")]
        public string Error { get; set; }
    }
}
