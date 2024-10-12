using Electronic_Application.Features.Order.Queries.GetOrderItemsById;
using MediatR;

namespace Electronic_Application.Features.Order.Queries.GetOrderById
{
    public class GetOrderItemsByIdQuery: IRequest<List<OrderItemListDto>>
    {
        public Guid Id { get; set; }
    }
}
