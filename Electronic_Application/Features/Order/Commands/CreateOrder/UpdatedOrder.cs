using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Electronic_Application.Features.Order.Commands.CreateOrder
{
    public class UpdatedOrder
    {
        public OrderDto Order { get; set; }
        public ShippingDetailDto ShippingDetail { get; set; }
    }
}
