using MediatR;

namespace Electronic_Application.Features.Auth.Commands.Login
{
    public class LoginUserCommand : IRequest<LoginDto>
    {
        public string Email { get; set; }
        public string Password { get; set; }

    }
}
