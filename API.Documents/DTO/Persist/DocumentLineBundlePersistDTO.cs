using API.Documents.Models;
using Newtonsoft.Json;

namespace API.Documents.DTO.Persist
{
    public class DocumentLineBundlePersistDTO
    {
        [JsonProperty("lb_bundle_id")]
        public int BundleId { get; set; }

        [JsonProperty("lb_quantity")]
        public int Quantity { get; set; }

        [JsonProperty("lb_unit_price")]
        public double UnitPrice { get; set; }

        [JsonProperty("lb_discount")]
        public double Discount { get; set; }

        [JsonProperty("lb_net_price")]
        public double NetPrice { get; set; }

        [JsonProperty("lb_total_price")]
        public double TotalPrice { get; set; }

        public List<int> BundleElements { get; } = new();
        
        public DocumentLineBundlePersistDTO()
        {
        }

        public DocumentLineBundlePersistDTO(DocumentLineBundle documentLineBundle)
        {
            this.BundleId = documentLineBundle.BundleId;
            this.UnitPrice = documentLineBundle.UnitPrice;
            this.Quantity = documentLineBundle.Quantity;
            this.Discount = documentLineBundle.Discount;
            this.NetPrice = documentLineBundle.NetPrice;
            this.TotalPrice = documentLineBundle.TotalPrice;
            this.BundleElements = documentLineBundle.BundleElements.Select(x => x.BundleElementId).ToList();
        }
    }
}
