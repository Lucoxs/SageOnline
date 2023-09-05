using API.IdentityServer.Models;

namespace API.IdentityServer.Interfaces
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(User user);
    }
}
