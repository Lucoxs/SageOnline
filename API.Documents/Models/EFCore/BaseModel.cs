using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Documents.Models.EFCore
{
    public class BaseModel
    {
        [Key]
        [Column(Order = 0)]
        public long Id { get; set; }

        [Column(Order = 1)]
        public string? HashId { get; set; }

        [Column("co_id", Order = 2)]
        public int CompanyId { get; set; }

        [Column("co_hash_id", Order = 3)]
        public string HashCompanyId { get; set; }

        [Column("us_creater_id", Order = 4)]
        public string UserIdCreater { get; set; }

        [Column("us_hash_creater_id", Order = 5)]
        public string HashUserIdCreater { get; set; }

        [Column(Order = 6)]
        public DateTime CreatedAt { get; set; }

        [Column(Order = 7)]
        public string HashCreatedAt { get; set; }

        [Column(Order = 8)]
        public DateTime? ModifiedAt { get; set; }

        [Column(Order = 9)]
        public string? HashModifiedAt { get; set; }

        [Column("us_modified_id", Order = 10)]
        public string? UsedIdModifier { get; set; }

        [Column("us_hash_modified_id", Order = 11)]
        public string? HashUsedIdModifier { get; set; }

        [Column(Order = 12)]
        public DateTime? DeletedAt { get; set; }

        [Column(Order = 13)]
        public string? HashDeletedAt { get; set; }

        [Column(Order = 14)]
        public bool IsDeleted { get; set; } = false;

        [Column("us_deleter_id", Order = 15)]
        public string? UserIdDeleter { get; set; }

        [Column("us_hash_deleter_id", Order = 16)]
        public string? HashUserIdDeleter { get; set; }
    }
}
