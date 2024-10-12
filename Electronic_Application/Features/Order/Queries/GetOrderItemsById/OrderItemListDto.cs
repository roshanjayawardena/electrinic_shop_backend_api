namespace Electronic_Application.Features.Order.Queries.GetOrderItemsById
{
    public class OrderItemListDto
    {
        public Guid Id { get; set; }
        public int Quantity { get; set; }
        public string ProductName { get; set; }
        public string ProductImage { get; set; }
        public double SubTotal { get; set; }
        public double UnitPrice { get; set; }
        public Guid OrderId { get; set; }
    }
}
