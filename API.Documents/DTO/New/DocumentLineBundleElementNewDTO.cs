using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Documents.DTO.New
{
    public class DocumentLineBundleElementNewDTO
    {
        [JsonProperty("le_bundle_id")]
        public int VariantId { get; set; }

        [JsonProperty("le_unit_price")]
        public double UnitPrice { get; set; }

        [JsonProperty("le_quantity")]
        public int Quantity { get; set; }

        [JsonProperty("le_discount")]
        public double Discount { get; set; }
    }
}
