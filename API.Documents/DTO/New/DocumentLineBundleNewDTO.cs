using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Documents.DTO.New
{
    public class DocumentLineBundleNewDTO
    {
        [JsonProperty("lb_bundle_id")]
        public int BundleId { get; set; }

        [JsonProperty("lb_total_price")]
        public double TotalPrice { get; set; }

        [JsonProperty("lb_bundle_id")]
        public List<DocumentLineBundleElementNewDTO> DocumentLineBundleElementNEWDTO = new();
    }
}
