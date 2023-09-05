using API.IdentityServer.DTO;

namespace API.IdentityServer.Interfaces
{
    public interface IAuthService
    {
        Task<UserDTO> Register(UserRegisterDTO userRegisterDTO);
        Task<UserResponseDTO> Login(UserLoginDTO userLogin);
    }
}
