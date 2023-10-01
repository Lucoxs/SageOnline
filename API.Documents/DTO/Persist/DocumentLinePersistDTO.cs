using API.Documents.DTO.New;
using API.Documents.Models;
using Newtonsoft.Json;

namespace API.Documents.DTO.Persist
{
    public class DocumentLinePersistDTO
    {
        [JsonProperty("dl_label")]
        public string? Label { get; set; }

        [JsonProperty("dl_is_bundle")]
        public bool IsBundle { get; set; }

        [JsonProperty("dl_variant", NullValueHandling = NullValueHandling.Ignore)]
        public DocumentLineVariantPersistDTO? DocumentLineVariantNewDTO { get; set; } = null;

        [JsonProperty("dl_bundle", NullValueHandling = NullValueHandling.Ignore)]
        public DocumentLineBundlePersistDTO? DocumentLineBundleNewDTO { get; set; } = null;

        public DocumentLinePersistDTO()
        {
            
        }

        public DocumentLinePersistDTO(DocumentLine documentLine)
        {
            this.Label = documentLine.Label;
            this.IsBundle = documentLine.IsBundle;
            if (this.IsBundle)
            {
                DocumentLineBundleNewDTO = new(documentLine.DocumentLineBundle);
            }
            else
            {
                DocumentLineVariantNewDTO = new(documentLine.DocumentLineVariant);
            }
        }
    }
}
