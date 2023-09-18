using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Documents.DTO.New
{
    public class DocumentLineVariantNewDTO
    {
        [JsonProperty("lv_product_id")]
        public int ProductId { get; set; }

        [JsonProperty("lv_variant_id")]
        public int VariantId { get; set; }

        [JsonProperty("lv_unit_price")]
        public double UnitPrice { get; set; }

        [JsonProperty("lv_quantity")]
        public int Quantity { get; set; }

        [JsonProperty("lv_discount")]
        public double Discount { get; set; }
    }
}
