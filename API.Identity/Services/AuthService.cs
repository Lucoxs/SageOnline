using API.Identity.Interfaces;
using Azure.Core;
using IdentityModel.Client;
using Newtonsoft.Json;
using Serilog;
using static System.Net.WebRequestMethods;

namespace API.Identity.Services
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly string _authority;
        private readonly HttpClient _httpClient = new();

        public AuthService(IConfiguration configuration)
        {
            _configuration = configuration;
            _authority = $"{_configuration["ASPNETCORE_URLS"]?.Replace("+", "localhost")}/";
            Log.Debug(_authority);
        }

        public async Task<HttpResponseMessage> SignIn(string client_id, string code)
        {
            Log.Information(client_id);
            Log.Information(_configuration[$"{client_id}:Secret"]);
            Log.Information(code);
            using FormUrlEncodedContent content = new(new List<KeyValuePair<string, string>>()
            {
                new("client_id", client_id),
                new("client_secret", _configuration[$"{client_id}:Secret"]),
                new("grant_type", "authorization_code"),
                new("code", code),
                new("redirect_uri", "http://localhost:3000/signin-oidc"),
            });
            var response = await _httpClient.PostAsync($"{_authority}connect/token", content);
            Log.Information(response.StatusCode.ToString());
            return response;
        }

        public async Task<HttpResponseMessage> RefreshToken(string client_id, string refresh_token)
        {
            using FormUrlEncodedContent content = new(new List<KeyValuePair<string, string>>()
            {
                new("client_id", client_id),
                new("client_secret", _configuration[$"{client_id}:Secret"]),
                new("grant_type", "refresh_token"),
                new("refresh_token", refresh_token),
            });
            return await _httpClient.PostAsync($"{_authority}connect/token", content);
        }

        public async Task<UserInfoResponse> GetUserInfoAsync(string token)
        {
            return await _httpClient.GetUserInfoAsync(new UserInfoRequest
            {
                Address = $"{_authority}connect/userinfo",
                Token = token
            });

            /*using HttpRequestMessage httpRequestMessage = new();
            httpRequestMessage.Headers.Add("Authorization", $"Bearer {token}");

            return await _httpClient.GetAsync($"{_authority}connect/userinfo");*/
        }

        public async Task<TokenRevocationResponse> RevokeAccessToken(string client_id, string access_token)
        {
            return await _httpClient.RevokeTokenAsync(new()
            {
                Address = $"{_authority}connect/revocation",
                ClientId = client_id,
                ClientSecret = _configuration[$"{client_id}:Secret"],
                Token = access_token,
                TokenTypeHint = "access_token"
            });
        }

        public async Task<TokenRevocationResponse> RevokeRefreshToken(string client_id, string refresh_token)
        {
            return await _httpClient.RevokeTokenAsync(new()
            {
                Address = $"{_authority}connect/revocation",
                ClientId = client_id,
                ClientSecret = _configuration[$"{client_id}:Secret"],
                Token = refresh_token,
                TokenTypeHint = "refresh_token"
            });
        }

        public void Dispose()
        {
            _httpClient.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
