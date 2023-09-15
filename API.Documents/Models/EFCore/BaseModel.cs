namespace API.Documents.Models.EFCore
{
    public class BaseModel
    {
        public Guid Id { get; set; }
        public Guid CompanyId { get; set; }
        public string UserId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? ModifiedAt{ get; set; }
        public string? UsedIdModifier { get; set; }
        public DateTime? DeletedAt { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
