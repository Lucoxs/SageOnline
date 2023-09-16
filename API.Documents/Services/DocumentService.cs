using API.Documents.Context;
using API.Documents.DTO.Documents;
using API.Documents.Enums;
using API.Documents.Models;
using API.Documents.Utils;
using Microsoft.EntityFrameworkCore.Metadata;

namespace API.Documents.Services
{
    public class DocumentService
    {
        private readonly AppDbContext _context;

        public DocumentService(AppDbContext context)
        {
            _context = context;
        }

        public void CheckUpdatedValues(Document document, DocumentPersistDTO documentPersistDTO)
        {
            if (document.CreatedAt != documentPersistDTO.CreatedAt)
                throw new Exception();

            if (document.ModifiedAt != documentPersistDTO.ModifiedAt)
                throw new Exception();

            if (document.DocumentType != documentPersistDTO.DocumentType)
                throw new Exception();

            if (document.DocumentNumber != documentPersistDTO.DocumentNumber)
                throw new Exception();

            if (document.Date != documentPersistDTO.Date)
                throw new Exception();

            if (document.WarehouseId != documentPersistDTO.WarehouseId)
                throw new Exception();

            if (document.ThirdAccountId != documentPersistDTO.ThirdAccountId)
                throw new Exception();
        }

        public string GenerateDocumentNumber(DocumentType documentType, int company_id)
        {
            var prefix = DocumentUtil.GetDocumentNumberPrefix(documentType);
            prefix += DateTime.Now.ToString("yyMM");

            var lastDocumentNumber = _context.Documents
                .Where(x => x.DocumentNumber.Substring(0, 7) == prefix && x.CompanyId == company_id)
                .OrderByDescending(y => y.Id)
                .Select(z => z.DocumentNumber)
                .FirstOrDefault();

            return $"{prefix}{DocumentUtil.IncrementDocumentNumber(lastDocumentNumber?[7..])}";
        }
    }
}
