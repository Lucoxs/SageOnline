using API.Documents.DTO.New;
using API.Documents.Models.EFCore;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Documents.Models
{
    [Table("lb_document_line_bundle")]
    public class DocumentLineBundle : BaseModel
    {
        [Column("lb_bundle_id")]
        public int BundleId { get; set; }

        [Column("lb_quantity")]
        public int Quantity { get; set; }

        [Column("lb_unit_price")]
        public double UnitPrice { get; set; }

        [Column("lb_discount")]
        public double Discount { get; set; }

        [Column("lb_net_price")]
        public double NetPrice { get; set; }

        [Column("lb_total_price")]
        public double TotalPrice { get; set; }

        public DocumentLine DocumentLine { get; set; } = null!;

        public ICollection<DocumentLineBundleElement> BundleElements { get; } = new List<DocumentLineBundleElement>();

        public DocumentLineBundle()
        {

        }

        public DocumentLineBundle(int company_id, string user_id, DocumentLineBundleNewDTO documentLineBundleNewDTO)
        {
            this.CompanyId = company_id;
            this.UserIdCreater = user_id;
            this.BundleId = documentLineBundleNewDTO.BundleId;
            this.UnitPrice = documentLineBundleNewDTO.UnitPrice ?? 0;
            this.Discount = documentLineBundleNewDTO.Discount ?? 0;
            this.Quantity = documentLineBundleNewDTO.Quantity;
            if (this.Discount <= 0)
                this.NetPrice = this.UnitPrice;
            else
                this.NetPrice = this.UnitPrice * (Math.Abs((this.Discount / 100) - 1));
            
            this.TotalPrice = this.NetPrice * this.Quantity;

            foreach (var bundleElement in documentLineBundleNewDTO.DocumentLineBundleElementNEWDTO)
                this.BundleElements.Add(new(company_id, user_id, bundleElement));
        }
    }
}
