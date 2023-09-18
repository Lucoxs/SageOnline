using API.Documents.Context;
using API.Documents.DTO.Persist;
using API.Documents.Enums;
using API.Documents.Models;
using API.Documents.Utils;
using Humanizer;
using Microsoft.EntityFrameworkCore.Metadata;
using Newtonsoft.Json;

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

        public async Task<bool> ExistWarehouse(int companyId, int warehouseId)
        {
            using HttpClient client = new();
            using HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get, $"{warehouseId}");
            requestMessage.Headers.Add("company_id", companyId.ToString());

            var response = await client.SendAsync(requestMessage);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> ExistThirdAccount(int companyId, int thirdAccountId)
        {
            using HttpClient client = new();
            using HttpRequestMessage requestMessage = new(HttpMethod.Get, $"{thirdAccountId}");
            requestMessage.Headers.Add("company_id", companyId.ToString());

            var response = await client.SendAsync(requestMessage);
            return response.IsSuccessStatusCode;
        }

        public async Task<dynamic?> GetThirdAccountFirstContacts(int companyId, int thirdAccountId)
        {
            using HttpClient client = new();
            using HttpRequestMessage requestMessage = new(HttpMethod.Get, $"{thirdAccountId}");
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
            using HttpRequestMessage requestMessage = new(HttpMethod.Get, $"{thirdAccountId}/{contactId}");
            requestMessage.Headers.Add("company_id", companyId.ToString());

            var response = await client.SendAsync(requestMessage);
            return response.IsSuccessStatusCode;
        }

        public async Task<dynamic?> GetThirdAccountFirstShippingAddresses(int companyId, int thirdAccountId)
        {
            using HttpClient client = new();
            using HttpRequestMessage requestMessage = new(HttpMethod.Get, $"{thirdAccountId}");
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
            using HttpRequestMessage requestMessage = new(HttpMethod.Get, $"{thirdAccountId}/{shippingAddressId}");
            requestMessage.Headers.Add("company_id", companyId.ToString());

            var response = await client.SendAsync(requestMessage);
            return response.IsSuccessStatusCode;
        }

        public async Task<dynamic?> GetProductVariant(int companyId, int productId, int variantId)
        {
            using HttpClient client = new();
            using HttpRequestMessage requestMessage = new(HttpMethod.Get, $"{productId}/{variantId}");
            requestMessage.Headers.Add("company_id", companyId.ToString());

            var response = await client.SendAsync(requestMessage);
            if (!response.IsSuccessStatusCode)
                return null;

            var responseString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<dynamic>(responseString);
        }

        public async Task<dynamic?> GetBundleElement(int companyId, int bundleId, int bundleElementId)
        {
            using HttpClient client = new();
            using HttpRequestMessage requestMessage = new(HttpMethod.Get, $"{bundleId}/{bundleElementId}");
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
            using HttpRequestMessage requestMessage = new(HttpMethod.Get, $"{warehouseId}/{variantId}");
            requestMessage.Headers.Add("company_id", companyId.ToString());

            var response = await client.SendAsync(requestMessage);
            if (!response.IsSuccessStatusCode)
                return null;

            var responseString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<dynamic>(responseString);
        }
    }
}
