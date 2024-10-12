using Electronic_Domain.Common;

namespace Electronic_Domain.Entities
{
    public class OrderItem: EntityBase
    {
        public Guid OrderId { get; set; }       
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        public Order Order { get; set; }
        public int Quantity { get; set; }
        public double SubTotal { get; set; }
    }
}
