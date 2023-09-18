using API.Documents.Models.EFCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Documents.Models
{
    [Table("le_document_line_bundle_element")]
    public class DocumentLineBundleElement : BaseModel
    {
        [Column("le_bundle_id")]
        public int BundleElementId{ get; set; }

        [Column("le_bundle_name")]
        public string BundleElementName { get; set; }

        [Column("le_unit_price")]
        public double UnitPrice { get; set; }

        [Column("le_quantity")]
        public int Quantity { get; set; }

        [Column("le_discount")]
        public double Discount { get; set; }

        [Column("le_total_price")]
        public double TotalPrice { get; set; }

        public DocumentLineBundle DocumentLineBundle { get; set; } = null!;
    }
}
