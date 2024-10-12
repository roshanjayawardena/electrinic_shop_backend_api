using FluentValidation;

namespace Electronic_Application.Features.Auth.Commands.Login
{
    public class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
    {
        public LoginUserCommandValidator()
        {

            RuleFor(x => x.Email).NotEmpty().WithMessage("Email cannot be empty.")
              .NotNull().WithMessage("Email is required.")
              .EmailAddress().WithMessage("Please enter the valid email address.")
              .MaximumLength(50).WithMessage("Max length exceeded.");

            RuleFor(x => x.Password).NotEmpty().WithMessage("Password cannot be empty.")
                .NotNull().WithMessage("Password is required.")
                .MaximumLength(20).WithMessage("Max length exceeded.");
        }
    }
}
