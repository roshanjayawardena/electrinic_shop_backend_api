using MediatR;

namespace Electronic_Application.Features.Order.Commands.CreateOrder
{
    public class CreateOrderCommand: IRequest<Guid>
    {
        public OrderDto Order { get; set; }
        public ShippingDetailDto ShippingDetail { get; set; }
    }
}
