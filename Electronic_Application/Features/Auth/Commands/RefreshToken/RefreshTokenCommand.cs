using Electronic_Application.Features.Auth.Commands.Login;
using MediatR;

namespace Electronic_Application.Features.Auth.Commands.RefreshToken
{
    public class RefreshTokenCommand : IRequest<LoginDto>
    {
        public string RefreshToken { get; set; }
    }
}
