using API.Documents.Enums;
using API.Documents.Models.EFCore;

namespace API.Documents.Models
{
    public class Document : BaseModel
    {
        public string DocumentNumber { get; set; }
        public DocumentType DocumentType { get; set; }
        public long Warehouse { get; set; }

    }
}
