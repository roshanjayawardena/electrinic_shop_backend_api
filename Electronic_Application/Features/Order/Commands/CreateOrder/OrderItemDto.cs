namespace Electronic_Application.Features.Order.Commands.CreateOrder
{
    public class OrderItemDto
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public double SubTotal { get; set; }
    }
}
