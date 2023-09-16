using API.Documents.DTO.Documents;
using API.Documents.Enums;
using API.Documents.Models.EFCore;
using System.ComponentModel.DataAnnotations;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Xml.Linq;

namespace API.Documents.Models
{
    public class Document : BaseModel
    {
        public string DocumentNumber { get; set; }
        public string HashDocumentNumber { get; set; }
        public DocumentType DocumentType { get; set; }
        public DateTime Date { get; set; }
        public DateTime? ShippingDate { get; set; }
        public int ShippingAddressId { get; set; }
        public int WarehouseId { get; set; }
        public int ThirdAccountId { get; set; }
        public int ContactId { get; set; }

        public Document()
        {
            
        }

        public Document(int company_id, string user_id, string documentNumber, DocumentNewDTO documentNewDTO)
        {
            this.CompanyId = company_id;
            this.UserIdCreater = user_id;
            this.DocumentNumber = documentNumber;
            this.DocumentType = documentNewDTO.DocumentType;
            this.Date = documentNewDTO.Date;
            this.ShippingDate = documentNewDTO.ShippingDate;
            this.ShippingAddressId = documentNewDTO.ShippingAddressId;
            this.WarehouseId = documentNewDTO.WarehouseId;
            this.ThirdAccountId = documentNewDTO.ThirdAccountId;
            this.ContactId = documentNewDTO.ContactId;
        }

        public void SetUpdatedDocument(string user_id, DocumentPersistDTO documentDTO)
        {
            this.DocumentNumber = documentDTO.DocumentNumber;
            this.DocumentType = documentDTO.DocumentType;
            this.Date = documentDTO.Date;
            this.ShippingDate = documentDTO.ShippingDate;
            this.ShippingAddressId = documentDTO.ShippingAddressId;
            this.WarehouseId = documentDTO.WarehouseId;
            this.ThirdAccountId = documentDTO.ThirdAccountId;
            this.ContactId = documentDTO.ContactId;
            this.UsedIdModifier = user_id;
        }

        public void SetDeletedDocument(string user_id)
        {
            this.UserIdDeleted = user_id;
        }
    }
}
