using API.Identity.Interfaces;
using Azure.Core;
using IdentityModel.Client;

namespace API.Identity.Services
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly string _authority;
        private readonly HttpClient _httpClient = new();
        private readonly DiscoveryDocumentResponse _discoveryDocument;

        public AuthService(IConfiguration configuration)
        {
            _configuration = configuration;
            _authority = _configuration["ASPNETCORE_URLS"] ?? throw new Exception("Invalid program URL");
            _discoveryDocument = _httpClient.GetDiscoveryDocumentAsync(_authority).GetAwaiter().GetResult();
        }

        public async Task<TokenResponse> SignIn(string client_id, string code)
        {            
            return await _httpClient.RequestAuthorizationCodeTokenAsync(new()
            {
                Address = _discoveryDocument.TokenEndpoint,
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
                Address = _discoveryDocument.TokenEndpoint,
                ClientId = client_id,
                ClientSecret = _configuration[$"{client_id}:Secret"],
                RefreshToken = refresh_token
            });
        }

        public async Task<UserInfoResponse> GetUserInfoAsync(string token)
        {
            return await _httpClient.GetUserInfoAsync(new UserInfoRequest
            {
                Address = _discoveryDocument.UserInfoEndpoint,
                Token = token
            });
        }

        public async Task<TokenRevocationResponse> RevokeAccessToken(string client_id, string access_token)
        {
            return await _httpClient.RevokeTokenAsync(new()
            {
                Address = _discoveryDocument.RevocationEndpoint,
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
                Address = _discoveryDocument.RevocationEndpoint,
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
