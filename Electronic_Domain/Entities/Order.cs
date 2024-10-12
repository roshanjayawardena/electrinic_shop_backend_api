using Electronic_Domain.Common;
using Electronic_Domain.Common.Enums;

namespace Electronic_Domain.Entities
{
    public class Order: EntityBase
    {
        public double Total { get; set; }
        public DateTime OrderDate { get; set; }
        public OrderStatus Status { get; set; }
        public ShippingDetail ShippingDetail { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
        public ICollection<Product> Products { get; set; }

    }
}
