using API.Documents.Enums;
using API.Documents.Models.EFCore;
using System.ComponentModel.DataAnnotations;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Xml.Linq;
using System.ComponentModel.DataAnnotations.Schema;
using API.Documents.DTO.New;
using API.Documents.DTO.Persist;

namespace API.Documents.Models
{
    [Table("do_document")]
    public class Document : BaseModel
    {
        [Column("do_document_number")]
        public string DocumentNumber { get; set; }

        [Column("do_hash_document_number")]
        public string HashDocumentNumber { get; set; }

        [Column("do_document_type")]
        public DocumentType DocumentType { get; set; }

        [Column("do_date")]
        public DateTime Date { get; set; }

        [Column("do_shipping_date")]
        public DateTime? ShippingDate { get; set; }

        [Column("do_shipping_address_id")]
        public int ShippingAddressId { get; set; }

        [Column("do_warehouse_id")]
        public int WarehouseId { get; set; }

        [Column("do_warehouse_destination_id")]
        public int? WarehouseDestinationId { get; set; }

        [Column("do_third_account_id")]
        public int ThirdAccountId { get; set; }

        [Column("do_contact_id")]
        public int ContactId { get; set; }

        public ICollection<DocumentLine> Lines { get; } = new List<DocumentLine>();

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
            this.ShippingAddressId = documentNewDTO.ShippingAddressId ?? 0;
            this.WarehouseId = documentNewDTO.WarehouseId;
            this.WarehouseDestinationId = documentNewDTO.WarehouseDestinationId;
            this.ThirdAccountId = documentNewDTO.ThirdAccountId;
            this.ContactId = documentNewDTO.ContactId ?? 0;

            foreach(var line in documentNewDTO.DocumentLineNewDTOs)
                Lines.Add(new DocumentLine(company_id, user_id, line));
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
            this.UserIdDeleter = user_id;
        }
    }
}
