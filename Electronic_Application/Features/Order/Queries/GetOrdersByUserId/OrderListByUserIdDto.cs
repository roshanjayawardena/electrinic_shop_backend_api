﻿using Electronic_Domain.Common.Enums;

namespace Electronic_Application.Features.Order.Queries.GetOrdersByUserId
{
    public class OrderListByUserIdDto
    {
        public Guid Id { get; set; }
        public string CustomerName { get; set; }
        public DateTime OrderDate { get; set; }
        public double Total { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}