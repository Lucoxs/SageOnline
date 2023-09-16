using API.Documents.DTO.EFCore;
using API.Documents.Enums;
using API.Documents.Models;

namespace API.Documents.DTO.Documents
{
    public class DocumentPersistDTO : BaseModelDTO
    {
        public string DocumentNumber { get; set; }
        public DocumentType DocumentType { get; set; }
        public DateTime Date { get; set; }
        public DateTime? ShippingDate { get; set; }
        public int ShippingAddressId { get; set; }
        public int WarehouseId { get; set; }
        public int ThirdAccountId { get; set; }
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
