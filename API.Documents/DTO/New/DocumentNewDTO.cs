using API.Documents.Enums;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Documents.DTO.New
{
    public class DocumentNewDTO
    {
        [JsonProperty("do_document_type")]
        public DocumentType DocumentType { get; set; }

        [JsonProperty("do_date")]
        public DateTime Date { get; set; }

        [JsonProperty("do_shipping_date")]
        public DateTime? ShippingDate { get; set; }

        [JsonProperty("do_shipping_address_id")]
        public int? ShippingAddressId { get; set; }

        [JsonProperty("do_warehouse_id")]
        public int WarehouseId { get; set; }

        [JsonProperty("do_warehouse_destination_id")]
        public int? WarehouseDestinationId { get; set; }

        [JsonProperty("do_third_account_id")]
        public int ThirdAccountId { get; set; }

        [JsonProperty("do_contact_id")]
        public int? ContactId { get; set; }

        public List<DocumentLineNewDTO> DocumentLineNewDTOs { get; set; } = new();
    }
}
