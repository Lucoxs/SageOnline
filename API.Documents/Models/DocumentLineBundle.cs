using API.Documents.Models.EFCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Documents.Models
{
    [Table("lb_document_line_bundle")]
    public class DocumentLineBundle : BaseModel
    {
        [Column("lb_bundle_id")]
        public int BundleId { get; set; }

        [Column("lb_bundle_name")]
        public string BundleName { get; set; }

        [Column("lb_total_price")]
        public double TotalPrice { get; set; }

        public DocumentLine DocumentLine { get; set; } = null!;

        public ICollection<DocumentLineBundleElement> BundleElements { get; } = new List<DocumentLineBundleElement>();
    }
}
