using FluentValidation;

namespace Electronic_Application.Features.Order.Commands.CreateOrder
{
    public class OrderItemDtoValidator : AbstractValidator<OrderItemDto>
    {
        public OrderItemDtoValidator()
        {
            RuleFor(p => p.ProductId)
                .NotEmpty().WithMessage("Product Id is required.");

            RuleFor(p => p.Quantity)
                .NotEmpty().WithMessage("Quantity is required.")
                .GreaterThan(0).WithMessage("Quantity must be greater than 0.");

            RuleFor(p => p.SubTotal)
                .NotEmpty().WithMessage("SubTotal is required.")
                .GreaterThan(0).WithMessage("SubTotal must be greater than 0.");
        }
    }
}
