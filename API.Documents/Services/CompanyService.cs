namespace API.Documents.Services
{
    public class CompanyService
    {
        public static async Task<bool> ExistCompany(int companyId)
        {
            using HttpClient httpClient = new();
            using HttpRequestMessage httpRequest = new(HttpMethod.Get, $"https://localhost:7001/api/v1/companies/{companyId}");
            var response = await httpClient.SendAsync(httpRequest);
            return response.IsSuccessStatusCode;
        }
    }
}
