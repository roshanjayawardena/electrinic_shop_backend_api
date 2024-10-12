using AutoMapper;
using Electronic_Application.Contracts.Infastructure.Auth;
using Electronic_Application.Models.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Electronic_Application.Features.Order.Queries.GetOrdersByUserId
{
    public class GetOrdersByUserIdQueryHandler : IRequestHandler<GetOrdersByUserIdQuery, PaginationResponse<List<OrderListByUserIdDto>>>
    {
        private readonly IApplicationDBContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;

        public GetOrdersByUserIdQueryHandler(IApplicationDBContext dbContext, IMapper mapper, ICurrentUserService currentUserService)
        {
            //_productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _currentUserService = currentUserService;   
        }
        public async Task<PaginationResponse<List<OrderListByUserIdDto>>> Handle(GetOrdersByUserIdQuery request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId;         

            var ordersList = _dbContext.Orders.Include(w => w.ShippingDetail).Where(w=>w.CreatedBy == userId)
                .Select(w => new OrderListByUserIdDto()
                {
                    Id = w.Id,
                    CustomerName = w.ShippingDetail.RecipientName,
                    OrderDate = w.OrderDate,
                    Total = w.Total,
                    Status = w.Status,
                    CreatedDate = w.CreatedDate      
                }).AsNoTracking().AsQueryable();

            int totalRecords = await ordersList.CountAsync();
            var itemDetalQuery = ordersList.Skip(request.SkipPageCount).Take(request.PageSize);

            var orderList = _mapper.Map<List<OrderListByUserIdDto>>(itemDetalQuery);
            return new PaginationResponse<List<OrderListByUserIdDto>>(orderList, totalRecords);
        }
    }
}
