using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Documents.Context;
using API.Documents.Models;
using API.Documents.Services;
using API.Documents.DTO.New;
using API.Documents.DTO.Persist;
using Microsoft.Identity.Client;
using API.Documents.Enums;
using Serilog;

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
            Log.Information("documentssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssss");
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

            if (!await _documentService.ExistWarehouse(company_id, documentNewDTO.WarehouseId))
                throw new Exception("warehouse doesn't exist");

            if (documentNewDTO.DocumentType == DocumentType.StockTransfert)
            {
                //TODO Check destination warehouse
                //origine != destination 
                //destination exist
            }

            if (!await _documentService.ExistThirdAccount(company_id, documentNewDTO.ThirdAccountId))
                throw new Exception("third account doesn't exist");
            
            if (documentNewDTO.ContactId == null)
            {
                var contacts = await _documentService.GetThirdAccountFirstContacts(company_id, documentNewDTO.ThirdAccountId);
                documentNewDTO.ContactId = contacts?.Id;
            }
            else
            {
                if (!await _documentService.ExistThirdAccountContact(company_id, documentNewDTO.ThirdAccountId, documentNewDTO.ContactId))
                    throw new Exception("contact doesn't exist");
            }


            if (documentNewDTO.ShippingAddressId == null)
            {
                var shippingAddresses = await _documentService.GetThirdAccountFirstShippingAddresses(company_id, documentNewDTO.ThirdAccountId);
                documentNewDTO.ShippingAddressId = shippingAddresses?.Id;
            }
            else
            {
                if (!await _documentService.ExistThirdAccountShippingAddress(company_id, documentNewDTO.ThirdAccountId, documentNewDTO.ShippingAddressId))
                    throw new Exception("shipping adrress doesn't exist");
            }

            Dictionary<int, int> variantQuantities = new();
            foreach (var documentLine in documentNewDTO.DocumentLineNewDTOs)
            {
                if (!documentLine.IsBundle)
                {
                    var product = await _documentService.GetProductVariant(company_id, documentLine.DocumentLineVariantNewDTO.ProductId, documentLine.DocumentLineVariantNewDTO.VariantId)
                        ?? throw new Exception("product doesn't exist");

                    if (variantQuantities.ContainsKey(documentLine.DocumentLineVariantNewDTO.VariantId))
                        variantQuantities[documentLine.DocumentLineVariantNewDTO.VariantId] += documentLine.DocumentLineVariantNewDTO.Quantity;
                    else
                        variantQuantities.Add(documentLine.DocumentLineVariantNewDTO.VariantId, documentLine.DocumentLineVariantNewDTO.Quantity);
                }
                else
                {
                    foreach(var documentLineBundleElement in documentLine.DocumentLineBundleNewDTO.DocumentLineBundleElementNEWDTO)
                    {
                        var bundleElement = await _documentService.GetBundleElement(company_id, documentLine.DocumentLineBundleNewDTO.BundleId, documentLineBundleElement.VariantId);

                        if (variantQuantities.ContainsKey(documentLineBundleElement.VariantId))
                            variantQuantities[documentLineBundleElement.VariantId] += documentLineBundleElement.Quantity;
                        else
                            variantQuantities.Add(documentLineBundleElement.VariantId, documentLineBundleElement.Quantity);
                    }
                }
            }

            bool hasCheckStock = documentNewDTO.DocumentType is DocumentType.VentePrepaLivraison
                or DocumentType.VenteLivraison
                or DocumentType.VenteFacture
                or DocumentType.AchatDemande
                or DocumentType.AchatCommande
                or DocumentType.AchatFacture
                or DocumentType.StockSortie
                or DocumentType.StockTransfert;

            if (hasCheckStock)
            {
                foreach (var variantQuantity in variantQuantities)
                {
                    var stock = await _documentService.GetStock(company_id, documentNewDTO.WarehouseId, variantQuantity.Key);
                    if (stock.Quantity < variantQuantity.Value)
                        throw new Exception("not enough stock");
                }
            }

            var documentNumber = _documentService.GenerateDocumentNumber(documentNewDTO.DocumentType, company_id);
            var document = new Document(company_id, user_id, documentNumber, documentNewDTO);

            bool hasSetStock = documentNewDTO.DocumentType is not DocumentType.VenteDevis
                and not DocumentType.VenteCommande
                and not DocumentType.VenteAvoir
                and not DocumentType.VenteFactureAvoir
                and not DocumentType.AchatDemande
                and not DocumentType.AchatCommande
                and not DocumentType.AchatAvoir
                and not DocumentType.AchatFactureAvoir;

            if (hasSetStock)
            {
                foreach (var variantQuantity in variantQuantities)
                {
                    var stock = await _documentService.SetStock(company_id, documentNewDTO.WarehouseId, variantQuantity.Key, variantQuantity.Value);
                    if (stock < variantQuantity.Value)
                        throw new Exception("not enough stock");

                    if (documentNewDTO.DocumentType == DocumentType.StockTransfert)
                    {
                        //TODO set destination stock
                        //origine != destination 
                        //destination exist
                    }
                }
            }

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

        // DELETE: api/Documents/5
       /* [HttpDelete("{documentId}/lines/{lineId}")]
        public async Task<IActionResult> DeleteDocument(long documentId, [FromHeader] int company_id, [FromHeader] string user_id)
        {
            var document = await _context.Documents.FindAsync(documentId);
            if (document == null)
                return NotFound();

            if (!await CompanyService.ExistCompany(company_id) || !await UserService.ExistUser(company_id, user_id))
                return NotFound("Company or user doesn't exist");

            document.SetDeletedDocument(user_id);

            _context.Documents.Remove(document);
            await _context.SaveChangesAsync();

            return NoContent();
        }*/

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
