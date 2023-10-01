using API.Documents.DTO.New;
using API.Documents.Models.EFCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Documents.Models
{
    [Table("dl_document_line")]
    public class DocumentLine : BaseModel
    {
        [Column("dl_label")]
        public string Label { get; set; }

        public DocumentLineVariant? DocumentLineVariant { get; set; }

        public DocumentLineBundle? DocumentLineBundle { get; set; }

        [Column("dl_is_bundle")]
        public bool IsBundle { get; set; }

        public Document Document { get; set; } = null!;

        public DocumentLine()
        {
            
        }

        public DocumentLine(int company_id, string user_id, DocumentLineNewDTO documentLineNewDTO)
        {
            this.CompanyId = company_id;
            this.UserIdCreater = user_id;
            this.Label = documentLineNewDTO.Label;
            this.IsBundle = documentLineNewDTO.IsBundle;
            if (documentLineNewDTO.IsBundle)
                DocumentLineBundle = new(company_id, user_id, documentLineNewDTO.DocumentLineBundleNewDTO);
            else
                DocumentLineVariant = new(company_id, user_id, documentLineNewDTO.DocumentLineVariantNewDTO);
        }
    }
}
