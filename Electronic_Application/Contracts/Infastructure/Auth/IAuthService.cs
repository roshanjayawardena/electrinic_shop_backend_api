using Electronic_Application.Features.Auth.Commands.Login;
using Electronic_Application.Features.Auth.Commands.RefreshToken;
using Electronic_Application.Models.Auth;

namespace Electronic_Application.Contracts.Infastructure.Auth
{
    public interface IAuthService
    {       
        Task<LoginDto> Login(LoginUser user);
        Task<bool> RegisterUser(RegisterUserModel user);
        Task<LoginDto> RenewTokens(RefreshTokenDto refreshToken);
    }
}
