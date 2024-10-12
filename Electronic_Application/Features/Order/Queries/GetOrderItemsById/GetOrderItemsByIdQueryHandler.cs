using AutoMapper;
using Electronic_Application.Contracts.Infastructure.Auth;
using Electronic_Application.Exceptions;
using Electronic_Application.Features.Order.Queries.GetOrderItemsById;
using FluentValidation.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Electronic_Application.Features.Order.Queries.GetOrderById
{
    public class GetOrderItemsByIdQueryHandler : IRequestHandler<GetOrderItemsByIdQuery, List<OrderItemListDto>>
    {
        // private readonly IProductRepository _productRepository;
        private readonly IApplicationDBContext _dbContext;
        private readonly IMapper _mapper;

        public GetOrderItemsByIdQueryHandler(IApplicationDBContext dbContext, IMapper mapper)
        {
            //_productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<List<OrderItemListDto>> Handle(GetOrderItemsByIdQuery request, CancellationToken cancellationToken)
        {
            if (request.Id == Guid.Empty)
            {
                throw new ValidationException(new ValidationFailure("OrderId", "Invalid order id."));
            }

            var orderItems = _dbContext.OrderItems.Include(w=>w.Product).Where(p => p.OrderId == request.Id).AsNoTracking().AsQueryable();
            return _mapper.Map<List<OrderItemListDto>>(await orderItems.ToListAsync());
        }
    }
}
