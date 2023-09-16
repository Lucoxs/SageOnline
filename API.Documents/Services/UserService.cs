namespace API.Documents.Services
{
    public class UserService
    {
        public static async Task<bool> ExistUser(int companyId, string userId)
        {
            using HttpClient httpClient = new();
            using HttpRequestMessage httpRequest = new(HttpMethod.Get, $"https://localhost:7001/api/v1/companies/{companyId}/users/{userId}");
            var response = await httpClient.SendAsync(httpRequest);
            return response.IsSuccessStatusCode;
        }
    }
}
