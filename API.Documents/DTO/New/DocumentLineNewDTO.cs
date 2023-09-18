using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Documents.DTO.New
{
    public class DocumentLineNewDTO
    {
        [JsonProperty("dl_label")]
        public string Label { get; set; }

        [JsonProperty("dl_is_bundle")]
        public bool IsBundle { get; set; }

        [JsonProperty("dl_variant")]
        public DocumentLineVariantNewDTO DocumentLineVariantNewDTO { get; set; } = new();

        [JsonProperty("dl_bundle")]
        public DocumentLineBundleNewDTO DocumentLineBundleNewDTO { get; set; } = new();
    }
}
