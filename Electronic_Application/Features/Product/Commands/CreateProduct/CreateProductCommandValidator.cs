using FluentValidation;

namespace Electronic_Application.Features.Product.Commands.CreateProduct
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(p => p.Name)
               .NotEmpty().WithMessage("{Name} is required.")
               .NotNull();

            RuleFor(p => p.Price)
               .NotEmpty().WithMessage("{Price} is required.")
               .NotNull();

            RuleFor(p => p.CategoryId)
              .NotEmpty().WithMessage("{Category} is required.")
              .NotNull();

            RuleFor(p => p.Brand)
             .NotEmpty().WithMessage("{Brand} is required.")
             .NotNull();

            RuleFor(p => p.Description)
             .NotEmpty().WithMessage("{Description} is required.")
             .NotNull();

            RuleFor(p => p.Description)
           .NotEmpty().WithMessage("{Description} is required.")
           .NotNull();

           RuleFor(p => p.Image)
           .NotNull()
          .WithMessage("File is required.");       

            //.MaximumLength(50).WithMessage("{Name} must not exceed 50 characters.");
        }
    }
}
