using API.Documents.Models.EFCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Documents.Models
{
    [Table("lv_document_line_variant")]
    public class DocumentLineVariant : BaseModel
    {
        [Column("lv_product_id")]
        public int ProductId { get; set; }

        [Column("lv_product_name")]
        public string ProductName { get; set; }

        [Column("lv_variant_id")]
        public int VariantId { get; set; }

        [Column("lv_variant_name")]
        public string VariantName { get; set; }

        [Column("lv_unit_price")]
        public double UnitPrice { get; set; }

        [Column("lv_quantity")]
        public int Quantity { get; set; }

        [Column("lv_discount")]
        public double Discount { get; set; }

        [Column("lv_total_price")]
        public double TotalPrice { get; set; }

        public DocumentLine DocumentLine { get; set; } = null!;
    }
}
