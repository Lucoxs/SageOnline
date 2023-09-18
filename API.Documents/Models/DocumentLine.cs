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
    }
}
