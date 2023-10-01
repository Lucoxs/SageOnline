using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Documents.DTO.New
{
    public class DocumentLineBundleElementNewDTO
    {
        [JsonProperty("le_bundle_id")]
        public int VariantId { get; set; }
    }
}
