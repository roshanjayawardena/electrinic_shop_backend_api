using Electronic_Application.Models.Common;
using MediatR;

namespace Electronic_Application.Features.Order.Queries.GetOrdersByUserId
{
    public class GetOrdersByUserIdQuery : PaginationQuery, IRequest<PaginationResponse<List<OrderListByUserIdDto>>>
    {
       
    }
}
