using AutoMapper;
using Electronic_Application.Contracts.Infastructure.Auth;
using Electronic_Application.Features.Product.Queries.GetAllProducts;
using Electronic_Application.Models.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Electronic_Application.Features.Order.Queries.GetAllOrders
{
    public class GetAllOrdersRequestHandler : IRequestHandler<GetAllOrdersRequest, PaginationResponse<List<OrderListDto>>>
    {      
        private readonly IApplicationDBContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;

        public GetAllOrdersRequestHandler(IApplicationDBContext dbContext, IMapper mapper, ICurrentUserService currentUserService)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _currentUserService = currentUserService;
         }
        public async Task<PaginationResponse<List<OrderListDto>>> Handle(GetAllOrdersRequest request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId;

            var ordersList =  _dbContext.Orders.Include(w => w.ShippingDetail)
                .Select(w => new OrderListDto()
                {
                    Id = w.Id,
                    CustomerName = w.ShippingDetail.RecipientName,
                    OrderDate = w.OrderDate,
                    Total = w.Total,
                    Status = w.Status,
                    CreatedDate= w.CreatedDate,
                   
                }).AsNoTracking().AsQueryable();

            int totalRecords = await ordersList.CountAsync();
            var itemDetalQuery = ordersList.Skip(request.SkipPageCount).Take(request.PageSize);

            var orderList = _mapper.Map<List<OrderListDto>>(itemDetalQuery);
            return new PaginationResponse<List<OrderListDto>>(orderList, totalRecords);
        }
    }
}
