using FluentValidation;

namespace Electronic_Application.Features.Category.Commands.CreateCategory
{
    public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
    {
        public CreateCategoryCommandValidator()
        {
            RuleFor(p => p.Name)
               .NotEmpty().WithMessage("{Name} is required.")
               .NotNull();
            //.MaximumLength(50).WithMessage("{Name} must not exceed 50 characters.");
        }
    }
}
