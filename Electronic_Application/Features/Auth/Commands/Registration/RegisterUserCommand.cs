using MediatR;

namespace Electronic_Application.Features.Auth.Commands.Registration
{
    public class RegisterUserCommand : IRequest<bool>
    {
        //public string FirstName { get; set; }
        //public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; } = false;
        //public string ConfirmPassword { get; set; }
    }
}
