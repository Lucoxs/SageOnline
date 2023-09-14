using Duende.IdentityServer.Services;

namespace API.Identity.Services
{
    public class CorsPolicyService : ICorsPolicyService
    {
        public async Task<bool> IsOriginAllowedAsync(string origin)
        {
            return await Task.FromResult(true);
        }
    }
}
