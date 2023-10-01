using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Documents.DTO.New
{
    public class DocumentLineBundleNewDTO
    {
        [JsonProperty("lb_bundle_id")]
        public int BundleId { get; set; }

        [Column("lb_quantity")]
        public int Quantity { get; set; }

        [JsonProperty("lb_unit_price")]
        public double? UnitPrice { get; set; }

        [JsonProperty("lb_discount")]
        public double? Discount { get; set; }

        [JsonProperty("lb_bundle_id")]
        public List<DocumentLineBundleElementNewDTO> DocumentLineBundleElementNEWDTO = new();
    }
}
