﻿
using API.Identity.Models;
using Duende.IdentityServer.Models;
using IdentityModel.Client;

namespace API.Identity.Interfaces
{
    public interface IAuthService : IDisposable
    {
        public Task<TokenResponse> SignIn(string client_id, string code);
        public Task<TokenResponse> RefreshToken(string client_id, string refresh_token);
        public Task<UserInfoResponse> GetUserInfoAsync(string token);
        public Task<TokenRevocationResponse> RevokeAccessToken(string client_id, string access_token);
        public Task<TokenRevocationResponse> RevokeRefreshToken(string client_id, string refresh_token);
        public Task<TokenClaim> GetTokenClaimAsync(HttpRequest request);
    }
}
