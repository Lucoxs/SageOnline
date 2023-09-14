using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace API.Identity.Models
{
    [Table("co_company")]
    public class Company
    {
        [Key]
        [Column("co_id")]
        public Guid Id { get; set; }

        [Column("co_name")]
        [MaxLength(255)]
        public string Name { get; set; } = default!;

        [Column("co_activity")]
        [MaxLength(255)]
        public string? Activity { get; set; }

        [Column("co_legal_status")]
        [MaxLength(255)]
        public string? LegalStatus { get; set; }

        [Column("co_capital")]
        public double? Capital { get; set; }

        [Column("co_address")]
        [MaxLength(255)]
        public string? Address { get; set; }

        [Column("co_complement")]
        [MaxLength(255)]
        public string? Complement { get; set; }

        [Column("co_zip")]
        [MaxLength(255)]
        public string? Zip { get; set; }

        [Column("co_city")]
        [MaxLength(255)]
        public string? City { get; set; }

        [Column("co_region")]
        [MaxLength(255)]
        public string? Region { get; set; }

        [Column("co_country")]
        [MaxLength(255)]
        public string? Country { get; set; }

        [Column("co_siret")]
        [MaxLength(255)]
        public string? Siret { get; set; }

        [Column("co_vat_identifier")]
        [MaxLength(255)]
        public string? VatIdentifier { get; set; }

        [Column("co_naf_code")]
        [MaxLength(255)]
        public string? NafCode { get; set; }

        [Column("co_website")]
        [MaxLength(255)]
        public string? Website { get; set; }

        [Column("co_phone")]
        [MaxLength(255)]
        public string? Phone { get; set; }

        [Column("co_email")]
        [MaxLength(255)]
        public string? Email { get; set; }

        [Column("co_max_users")]
        [MaxLength(255)]
        public int MaxUsers { get; set; } = 1;

        public ICollection<User> Users { get; } = new List<User>();
    }
}
