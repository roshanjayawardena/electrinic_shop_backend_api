using Electronic_Application.Helpers;
using FluentValidation;

namespace Electronic_Application.Features.Auth.Commands.Registration
{
    public class RegisterUserCommandValidator: AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserCommandValidator() {

            //RuleFor(x => x.FirstName).NotEmpty().WithMessage("First Name cannot be empty.")
            //   .NotNull().WithMessage("First Name is required.");

            //RuleFor(x => x.LastName).NotEmpty().WithMessage("Last Name cannot be empty.")
            //    .NotNull().WithMessage("Last Name is required.");

            RuleFor(x => x.Email).NotEmpty().WithMessage("Email cannot be empty.")
              .NotNull().WithMessage("Email is required.")
              .EmailAddress().WithMessage("Please enter the valid email address.");

            RuleFor(x => x.Password).NotEmpty().WithMessage("Password cannot be empty.")
                   .NotNull().WithMessage("Password is required.")
                   .Must(ValidationHelper.IsValidPassword).WithMessage("Please enter valid password.");

            //RuleFor(x => x.ConfirmPassword).NotEmpty().WithMessage("ConfirmPassword cannot be empty.")
            //       .NotNull().WithMessage("ConfirmPassword is required.")
            //       .Equal(x => x.Password).WithMessage("Password do not Match.");
        }
    }
}
