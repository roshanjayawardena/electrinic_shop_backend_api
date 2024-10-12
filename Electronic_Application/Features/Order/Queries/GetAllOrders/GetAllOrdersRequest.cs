using Electronic_Application.Models.Common;
using MediatR;

namespace Electronic_Application.Features.Order.Queries.GetAllOrders
{
    public class GetAllOrdersRequest : PaginationQuery,IRequest<PaginationResponse<List<OrderListDto>>>
    {



    }
}
