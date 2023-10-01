using API.Documents.Models;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Documents.DTO.Persist
{
    public class DocumentLineVariantPersistDTO
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

        [JsonProperty("lv_net_price")]
        public double NetPrice { get; set; }

        [JsonProperty("lv_total_price")]
        public double TotalPrice { get; set; }

        public DocumentLineVariantPersistDTO()
        {
            
        }

        public DocumentLineVariantPersistDTO(DocumentLineVariant documentLineVariant)
        {
            this.ProductId = documentLineVariant.ProductId;
            this.VariantId = documentLineVariant.VariantId;
            this.UnitPrice = documentLineVariant.UnitPrice;   
            this.Quantity = documentLineVariant.Quantity;
            this.Discount = documentLineVariant.Discount;
            this.NetPrice = documentLineVariant.NetPrice;
            this.TotalPrice = documentLineVariant.TotalPrice;
        }
    }
}
