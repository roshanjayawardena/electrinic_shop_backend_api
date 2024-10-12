using Electronic_Domain.Common;

namespace Electronic_Domain.Entities
{
    public class ShippingDetail: EntityBase
    {
        public string RecipientName { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string Phone { get; set; }
        public Guid OrderId { get; set; }
        public Order Order { get; set; }
    }
}
