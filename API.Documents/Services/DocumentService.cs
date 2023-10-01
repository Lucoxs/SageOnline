using API.Documents.Context;
using API.Documents.DTO.New;
using API.Documents.DTO.Persist;
using API.Documents.Enums;
using API.Documents.Models;
using API.Documents.Models.Program;
using API.Documents.Utils;
using Humanizer;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Document = API.Documents.Models.Document;

namespace API.Documents.Services
{
    public class DocumentService
    {
        private readonly AppDbContext _context;
        private const string _baseUrlProduct = "http://localhost:3030/api/v1";
        private const string _baseUrlThirdAccounts = "http://localhost:3031";
        public DocumentService(AppDbContext context)
        {
            _context = context;
        }

        public void GetDocumentChildrens(Document document)
        {
            foreach (var line in document.Lines)
            {
                if (line.IsBundle)
                    line.DocumentLineBundle = _context.DocumentLineBundles.Include(y => y.BundleElements).FirstOrDefault(x => x.DocumentLine == line);
                else
                    line.DocumentLineVariant = _context.DocumentLineVariants.FirstOrDefault(x => x.DocumentLine == line);
            }
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

        #region CHECKING
        public async Task<List<CheckStock>> CheckDocument(DocumentNewDTO documentNewDTO, int company_id)
        {
            var domaine = DocumentUtil.GetDocumentDomaine(documentNewDTO.DocumentType);

            if (!await ExistWarehouse(company_id, documentNewDTO.WarehouseId))
                throw new Exception("warehouse doesn't exist");

            if (documentNewDTO.DocumentType == DocumentType.StockTransfert)
            {
                if (documentNewDTO.WarehouseDestinationId == null || !await ExistWarehouse(company_id, (int)documentNewDTO.WarehouseDestinationId))
                    throw new Exception("warehouse doesn't exist");
            }

            if (!await ExistThirdAccount(company_id, documentNewDTO.ThirdAccountId))
                throw new Exception("third account doesn't exist");

            if (documentNewDTO.ContactId == null)
            {
                var contacts = await GetThirdAccountFirstContacts(company_id, documentNewDTO.ThirdAccountId);
                documentNewDTO.ContactId = contacts?.Id;
            }
            else
            {
                if (!await ExistThirdAccountContact(company_id, documentNewDTO.ThirdAccountId, documentNewDTO.ContactId))
                    throw new Exception("contact doesn't exist");
            }


            if (documentNewDTO.ShippingAddressId == null)
            {
                var shippingAddresses = await GetThirdAccountFirstShippingAddresses(company_id, documentNewDTO.ThirdAccountId);
                documentNewDTO.ShippingAddressId = shippingAddresses?.Id;
            }
            else
            {
                if (!await ExistThirdAccountShippingAddress(company_id, documentNewDTO.ThirdAccountId, documentNewDTO.ShippingAddressId))
                    throw new Exception("shipping adrress doesn't exist");
            }

            List<CheckStock> variantQuantities = new();
            foreach (var documentLine in documentNewDTO.DocumentLineNewDTOs)
            {
                if (!documentLine.IsBundle)
                {
                    var variant = await GetProductVariant(company_id, documentLine.DocumentLineVariantNewDTO.ProductId, documentLine.DocumentLineVariantNewDTO.VariantId)
                        ?? throw new Exception("product or variant doesn't exist");

                    if (documentLine.DocumentLineVariantNewDTO.UnitPrice == null)
                    {
                        if (domaine == DomaineType.VENTE)
                            documentLine.DocumentLineVariantNewDTO.UnitPrice = variant.selling_price;
                        else if (domaine == DomaineType.ACHAT)
                            documentLine.DocumentLineVariantNewDTO.UnitPrice = variant.purchase_price;
                    }

                    documentLine.Label ??= (string)variant.name;

                    if (variant.stock_tracking == false)
                    {
                        if (domaine == DomaineType.STOCK)
                            throw new Exception("not stock traking on stock document");
                        else
                            continue;
                    }


                    var variantQuantity = variantQuantities.FirstOrDefault(x => x.Id == documentLine.DocumentLineVariantNewDTO.VariantId);
                    if (variantQuantity != null)
                        variantQuantity.Quantity += documentLine.DocumentLineVariantNewDTO.Quantity;
                    else
                        variantQuantities.Add(new(documentLine.DocumentLineVariantNewDTO.VariantId, (int)variant.stock, documentLine.DocumentLineVariantNewDTO.Quantity));
                }
                else
                {
                    var bundle = await GetBundleElement(company_id, documentLine.DocumentLineBundleNewDTO.BundleId)
                        ?? throw new Exception("bundle doesn't exist");

                    bool nullablePrice = false;
                    if (domaine == DomaineType.VENTE)
                    {
                        documentLine.DocumentLineBundleNewDTO.UnitPrice = bundle.price;
                    }
                    else if (domaine == DomaineType.ACHAT)
                    {
                        nullablePrice = documentLine.DocumentLineBundleNewDTO.UnitPrice != null;
                    }

                    documentLine.Label ??= (string)bundle.name;

                    foreach (var bundleElement in bundle.variants)
                    {
                        documentLine.DocumentLineBundleNewDTO.DocumentLineBundleElementNEWDTO.Add(new() { VariantId = (int)bundleElement.id });

                        if (nullablePrice)
                            documentLine.DocumentLineBundleNewDTO.UnitPrice += bundleElement.purchase_price;

                        if (bundleElement.stock_tracking == false)
                        {
                            if (domaine == DomaineType.STOCK)
                                throw new Exception("not stock traking on stock document");
                            else
                                continue;
                        }

                        var variantQuantity = variantQuantities.FirstOrDefault(x => x.Id == (int)bundleElement.id);
                        if (variantQuantity != null)
                            variantQuantity.Quantity += documentLine.DocumentLineBundleNewDTO.Quantity;
                        else
                            variantQuantities.Add(new((int)bundleElement.id, (int)bundleElement.stock, documentLine.DocumentLineBundleNewDTO.Quantity));
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
                foreach (var variantQuantity in variantQuantities)
                    if (variantQuantity.Quantity < variantQuantity.Stock)
                        throw new Exception("not enough stock");

            return variantQuantities;
        }
        #endregion

        #region PRODUCTS
        public async Task<bool> ExistWarehouse(int companyId, int warehouseId)
        {
            using HttpClient client = new();
            using HttpRequestMessage requestMessage = new(HttpMethod.Get, $"{_baseUrlProduct}/warehouses");
            requestMessage.Headers.Add("company_id", companyId.ToString());

            var response = await client.SendAsync(requestMessage);
            var responseString = await response.Content.ReadAsStringAsync();
            var warehouses = JsonConvert.DeserializeObject<List<dynamic>>(responseString);
            if (warehouses == null || warehouses.Count == 0)
                return false;

            return warehouses.Any(x => x.id == warehouseId);
        }

        public async Task<dynamic?> GetProductVariant(int companyId, int productId, int variantId)
        {
            using HttpClient client = new();
            using HttpRequestMessage requestMessage = new(HttpMethod.Get, $"{_baseUrlProduct}/products/{productId}");
            requestMessage.Headers.Add("company_id", companyId.ToString());
            
            var response = await client.SendAsync(requestMessage);
            if (!response.IsSuccessStatusCode)
                return null;

            var responseString = await response.Content.ReadAsStringAsync();
            var product = JObject.Parse(responseString);

            var variants = (product["variants"] as JArray) ?? new JArray();
            var variant = variants.FirstOrDefault(x => x["id"]?.ToString() == variantId.ToString());

            return JsonConvert.DeserializeObject<dynamic>(JsonConvert.SerializeObject(variant));
        }

        public async Task<dynamic?> GetBundleElement(int companyId, int bundleId)
        {
            using HttpClient client = new();
            using HttpRequestMessage requestMessage = new(HttpMethod.Get, $"{_baseUrlProduct}/bundles/{bundleId}");
            requestMessage.Headers.Add("company_id", companyId.ToString());

            var response = await client.SendAsync(requestMessage);
            if (!response.IsSuccessStatusCode)
                return null;

            var responseString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<dynamic>(responseString);
        }

        public async Task<dynamic?> GetStock(int companyId, int warehouseId, int variantId)
        {
            using HttpClient client = new();
            using HttpRequestMessage requestMessage = new(HttpMethod.Get, $"{warehouseId}/{variantId}");
            requestMessage.Headers.Add("company_id", companyId.ToString());

            var response = await client.SendAsync(requestMessage);
            if (!response.IsSuccessStatusCode)
                return null;

            var responseString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<dynamic>(responseString);
        }

        public async Task<dynamic?> SetStock(int companyId, int warehouseId, int variantId, int quantity)
        {
            using HttpClient client = new();
            using HttpRequestMessage requestMessage = new(HttpMethod.Get, $"{_baseUrlProduct}/variants/{variantId}/stocks");
            requestMessage.Headers.Add("company_id", companyId.ToString());

            var response = await client.SendAsync(requestMessage);
            if (!response.IsSuccessStatusCode)
                return null;

            var responseString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<dynamic>(responseString);
        }
        #endregion

        #region THIRD ACCOUNT
        public async Task<bool> ExistThirdAccount(int companyId, int thirdAccountId)
        {
            using HttpClient client = new();
            using HttpRequestMessage requestMessage = new(HttpMethod.Get, $"{_baseUrlThirdAccounts}/thirdAccounts/{thirdAccountId}");
            requestMessage.Headers.Add("company_id", companyId.ToString());

            var response = await client.SendAsync(requestMessage);
            return response.IsSuccessStatusCode;
        }

        public async Task<dynamic?> GetThirdAccountFirstContacts(int companyId, int thirdAccountId)
        {
            using HttpClient client = new();
            using HttpRequestMessage requestMessage = new(HttpMethod.Get, $"{_baseUrlThirdAccounts}/thirdAccounts/{thirdAccountId}/contact");
            requestMessage.Headers.Add("company_id", companyId.ToString());

            var response = await client.SendAsync(requestMessage);
            if (!response.IsSuccessStatusCode)
                return null;

            var responseString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<dynamic>>(responseString)?.FirstOrDefault();
        }

        public async Task<bool> ExistThirdAccountContact(int companyId, int thirdAccountId, int? contactId)
        {
            if (contactId == null)
                return false;

            using HttpClient client = new();
            using HttpRequestMessage requestMessage = new(HttpMethod.Get, $"{_baseUrlThirdAccounts}/thirdAccounts/{thirdAccountId}/contact/{contactId}");
            requestMessage.Headers.Add("company_id", companyId.ToString());

            var response = await client.SendAsync(requestMessage);
            return response.IsSuccessStatusCode;
        }

        public async Task<dynamic?> GetThirdAccountFirstShippingAddresses(int companyId, int thirdAccountId)
        {
            using HttpClient client = new();
            using HttpRequestMessage requestMessage = new(HttpMethod.Get, $"{_baseUrlThirdAccounts}/thirdAccounts/{thirdAccountId}/shippingAddress");
            requestMessage.Headers.Add("company_id", companyId.ToString());

            var response = await client.SendAsync(requestMessage);
            if (!response.IsSuccessStatusCode)
                return null;

            var responseString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<dynamic>>(responseString)?.FirstOrDefault();
        }

        public async Task<bool> ExistThirdAccountShippingAddress(int companyId, int thirdAccountId, int? shippingAddressId)
        {
            if (shippingAddressId == null)
                return false;

            using HttpClient client = new();
            using HttpRequestMessage requestMessage = new(HttpMethod.Get, $"{_baseUrlThirdAccounts}/thirdAccounts/{thirdAccountId}/shippingAddress/{shippingAddressId}");
            requestMessage.Headers.Add("company_id", companyId.ToString());

            var response = await client.SendAsync(requestMessage);
            return response.IsSuccessStatusCode;
        }
        #endregion
    }
}
