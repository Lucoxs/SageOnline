using API.Documents.DTO.EFCore;
using API.Documents.Enums;
using API.Documents.Models;
using Newtonsoft.Json;

namespace API.Documents.DTO.Persist
{
    public class DocumentPersistDTO : BaseModelDTO
    {
        public string DocumentNumber { get; set; }
        [JsonProperty("do_document_type")]
        public DocumentType DocumentType { get; set; }

        [JsonProperty("do_date")]
        public DateTime Date { get; set; }

        [JsonProperty("do_shipping_date")]
        public DateTime? ShippingDate { get; set; }

        [JsonProperty("do_shipping_address_id")]
        public int ShippingAddressId { get; set; }

        [JsonProperty("do_warehouse_id")]
        public int WarehouseId { get; set; }

        [JsonProperty("do_third_account_id")]
        public int ThirdAccountId { get; set; }

        [JsonProperty("do_contact_id")]
        public int ContactId { get; set; }

        public DocumentPersistDTO()
        {

        }

        public DocumentPersistDTO(Document document)
        {
            Id = document.Id;
            CreatedAt = document.CreatedAt;
            ModifiedAt = document.ModifiedAt;
            DocumentNumber = document.DocumentNumber;
            DocumentType = document.DocumentType;
            Date = document.Date;
            ShippingDate = document.ShippingDate;
            ShippingAddressId = document.ShippingAddressId;
            WarehouseId = document.WarehouseId;
            ThirdAccountId = document.ThirdAccountId;
            ContactId = document.ContactId;
        }
    }
}
