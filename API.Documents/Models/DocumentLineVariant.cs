using API.Documents.DTO.New;
using API.Documents.Models.EFCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Documents.Models
{
    [Table("lv_document_line_variant")]
    public class DocumentLineVariant : BaseModel
    {
        [Column("lv_product_id")]
        public int ProductId { get; set; }

        [Column("lv_variant_id")]
        public int VariantId { get; set; }

        [Column("lv_unit_price")]
        public double UnitPrice { get; set; }

        [Column("lv_quantity")]
        public int Quantity { get; set; }

        [Column("lv_discount")]
        public double Discount { get; set; }

        [Column("lv_net_price")]
        public double NetPrice { get; set; }

        [Column("lv_total_price")]
        public double TotalPrice { get; set; }

        public DocumentLine DocumentLine { get; set; } = null!;

        public DocumentLineVariant()
        {
            
        }

        public DocumentLineVariant(int company_id, string user_id, DocumentLineVariantNewDTO documentLineVariantNew)
        {
            this.CompanyId = company_id;
            this.UserIdCreater = user_id;
            this.ProductId = documentLineVariantNew.ProductId;
            this.VariantId = documentLineVariantNew.VariantId;
            this.Quantity = documentLineVariantNew.Quantity;
            this.UnitPrice = documentLineVariantNew.UnitPrice ?? 0;
            this.Discount = documentLineVariantNew.Discount ?? 0;

            if (this.Discount <= 0)
                this.NetPrice = this.UnitPrice;
            else
                this.NetPrice = this.UnitPrice * (Math.Abs((this.Discount / 100) - 1));

            this.TotalPrice = this.NetPrice * this.Quantity;
        }
    }
}
