using API.Documents.Enums;

namespace API.Documents.DTO.Documents
{
    public class DocumentNewDTO
    {
        public DocumentType DocumentType { get; set; }
        public DateTime Date { get; set; }
        public DateTime ShippingDate { get; set; }
        public int ShippingAddressId { get; set; }
        public int WarehouseId { get; set; }
        public int ThirdAccountId { get; set; }
        public int ContactId { get; set; }
    }
}
