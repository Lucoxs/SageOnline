using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Documents.Context;
using API.Documents.Models;
using API.Documents.DTO.Documents;
using API.Documents.Services;

namespace API.Documents.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DocumentsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly DocumentService _documentService;

        public DocumentsController(AppDbContext context, DocumentService documentService)
        {
            _context = context;
            _documentService = documentService;
        }

        // GET: api/Documents
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DocumentPersistDTO>>> GetDocuments([FromHeader] int company_id)
        {
            if (company_id <= 0)
                return NotFound();

            if (!await CompanyService.ExistCompany(company_id))
                return NotFound("Company doesn't exist");

            var documents = await _context.Documents.Where(x => x.CompanyId == company_id && !x.IsDeleted).ToListAsync();
            return Ok(documents.Select(x => new DocumentPersistDTO(x)));
        }

        // GET: api/Documents/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DocumentPersistDTO>> GetDocument(long id, [FromHeader] int company_id)
        {
            if (company_id <= 0)
                return NotFound();

            if (!await ExistDocumentAsync(id, company_id))
                return NotFound();

            if (!await CompanyService.ExistCompany(company_id))
                return NotFound("Company doesn't exist");

            var document = await GetDocumentAsync(id, company_id);
            var documentResponse = new DocumentPersistDTO(document);

            return Ok(documentResponse);
        }

        // PUT: api/Documents/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDocument(long id, [FromHeader] int company_id, [FromHeader] string user_id, [FromBody]DocumentPersistDTO documentDTO)
        {
            if (id != documentDTO.Id
                || company_id <= 0
                || string.IsNullOrWhiteSpace(user_id))
                return BadRequest();

            if (!await ExistDocumentAsync(id, company_id))
                return NotFound();

            if (!await CompanyService.ExistCompany(company_id) || !await UserService.ExistUser(company_id, user_id))
                return NotFound("Company or user doesn't exist");

            var document = await GetDocumentAsync(id, company_id);

            try
            {
                _documentService.CheckUpdatedValues(document, documentDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            document.SetUpdatedDocument(user_id, documentDTO);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return NoContent();
        }

        // POST: api/Documents
        [HttpPost]
        public async Task<ActionResult<Document>> PostDocument([FromHeader] int company_id, [FromHeader] string user_id, [FromBody] DocumentNewDTO documentNewDTO)
        {
            if (company_id <= 0
                || string.IsNullOrWhiteSpace(user_id))
                return BadRequest();

            if (!await CompanyService.ExistCompany(company_id) || !await UserService.ExistUser(company_id, user_id))
                return NotFound("Company or user doesn't exist");

            var documentNumber = _documentService.GenerateDocumentNumber(documentNewDTO.DocumentType, company_id);
            var document = new Document(company_id, user_id, documentNumber, documentNewDTO);

            _context.Documents.Add(document);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDocument", new { id = document.Id }, new DocumentPersistDTO(document));
        }

        // DELETE: api/Documents/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDocument(long id, [FromHeader] int company_id, [FromHeader] string user_id)
        {
            var document = await _context.Documents.FindAsync(id);
            if (document == null)
                return NotFound();

            if (!await CompanyService.ExistCompany(company_id) || !await UserService.ExistUser(company_id, user_id))
                return NotFound("Company or user doesn't exist");

            document.SetDeletedDocument(user_id);

            _context.Documents.Remove(document);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private async Task<bool> ExistDocumentAsync(long id, int company_id)
        {
            return await _context.Documents.AnyAsync(x => x.Id == id && x.CompanyId == company_id && !x.IsDeleted);
        }

        private async Task<Document> GetDocumentAsync(long id, int company_id)
        {
            return await _context.Documents.FirstAsync(x => x.Id == id && x.CompanyId == company_id);
        }
    }
}
