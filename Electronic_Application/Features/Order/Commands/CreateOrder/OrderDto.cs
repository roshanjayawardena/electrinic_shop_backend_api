using Electronic_Domain.Entities;

namespace Electronic_Application.Features.Order.Commands.CreateOrder
{
    public class OrderDto
    {
        public List<OrderItemDto> OrderItems { get; set; }
        public double OrderTotal { get; set; }
    }
}
