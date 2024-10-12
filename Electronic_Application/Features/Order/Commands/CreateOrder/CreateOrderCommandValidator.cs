using FluentValidation;

namespace Electronic_Application.Features.Order.Commands.CreateOrder
{
    public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderCommandValidator()
        {
            // rules for Shipping Detail
           RuleFor(p => p.ShippingDetail.Name)
              .NotEmpty().WithMessage("{Name} is required.")
              .NotNull();

           RuleFor(p => p.ShippingDetail.PostalCode)
               .NotEmpty().WithMessage("{PostalCode} is required.")
               .NotNull();

           RuleFor(p => p.ShippingDetail.Phone)
              .NotEmpty().WithMessage("{Phone} is required.")
              .NotNull();

           RuleFor(p => p.ShippingDetail.State)
             .NotEmpty().WithMessage("{State} is required.")
             .NotNull();

           RuleFor(p => p.ShippingDetail.AddressLine1)
             .NotEmpty().WithMessage("{AddressLine1} is required.")
             .NotNull();

           RuleFor(p => p.ShippingDetail.AddressLine2)
           .NotEmpty().WithMessage("{AddressLine2} is required.")
           .NotNull();

           RuleFor(p => p.ShippingDetail.City)
           .NotEmpty().WithMessage("{City} is required.")
           .NotNull();

            // rules for orderitems
           RuleFor(p => p.Order.OrderTotal)
            .NotEmpty().WithMessage("OrderTotal is required.")
            .GreaterThan(0).WithMessage("OrderTotal must be greater than 0.");

           RuleFor(p => p.Order.OrderItems)
            .NotEmpty().WithMessage("OrderItems are required.")
            .NotNull();

            // Use the custom validator for OrderItemDto inside OrderDto validator
            RuleForEach(p => p.Order.OrderItems)
                .SetValidator(new OrderItemDtoValidator());          
        }       
    }
}
