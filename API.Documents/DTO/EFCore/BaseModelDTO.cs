namespace API.Documents.DTO.EFCore
{
    public class BaseModelDTO
    {
        public long Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
    }
}
