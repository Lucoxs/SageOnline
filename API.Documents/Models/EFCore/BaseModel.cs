using System.ComponentModel.DataAnnotations;

namespace API.Documents.Models.EFCore
{
    public class BaseModel
    {
        [Key]
        public long Id { get; set; }
        public string? HashId { get; set; }
        public int CompanyId { get; set; }
        public string HashCompanyId { get; set; }
        public string UserIdCreater { get; set; }
        public string HashUserIdCreater { get; set; }
        public DateTime CreatedAt { get; set; }
        public string HashCreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public string? HashModifiedAt { get; set; }
        public string? UsedIdModifier { get; set; }
        public string? HashUsedIdModifier { get; set; }
        public DateTime? DeletedAt { get; set; }
        public string? HashDeletedAt { get; set; }
        public bool IsDeleted { get; set; } = false;
        public string? UserIdDeleted { get; set; }
        public string? HashUserIdDeleted { get; set; }
    }
}
