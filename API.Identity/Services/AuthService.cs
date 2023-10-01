using API.Identity.Interfaces;
using API.Identity.Models;
using Azure.Core;
using Duende.IdentityServer.Models;
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
            _authority = $"https://localhost:7001/";
            Log.Debug(_authority);
        }

        public async Task<TokenResponse> SignIn(string client_id, string code)
        {
            return await _httpClient.RequestAuthorizationCodeTokenAsync(new AuthorizationCodeTokenRequest
            {
                Address = $"{_authority}connect/token",

                ClientId = client_id,
                ClientSecret = _configuration[$"{client_id}:Secret"],

                Code = code,
                RedirectUri = "http://localhost:3000/signin-oidc"
            });
        }

        public async Task<TokenResponse> RefreshToken(string client_id, string refresh_token)
        {
            return await _httpClient.RequestRefreshTokenAsync(new()
            {
                Address = $"{_authority}connect/token",
                ClientId = client_id,
                ClientSecret = _configuration[$"{client_id}:Secret"],
                RefreshToken = refresh_token
            });
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

        public async Task<TokenClaim> GetTokenClaimAsync(HttpRequest request)
        {
            string? token = request.Headers["Authorization"].ToString()?.Replace("Bearer ", "");
            if (string.IsNullOrEmpty(token))
                throw new Exception("Empty claim");

            var userInfo = await GetUserInfoAsync(token);
            if (userInfo.IsError)
                throw new Exception("Empty claim");

            return JsonConvert.DeserializeObject<TokenClaim>(userInfo.Json.GetRawText()) 
                ?? throw new Exception("Empty claim");
        }

        public void Dispose()
        {
            _httpClient.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
