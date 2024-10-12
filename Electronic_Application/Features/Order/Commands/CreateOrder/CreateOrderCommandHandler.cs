using AutoMapper;
using Electronic_Application.Contracts.Persistence;
using Electronic_Application.Features.Category.Commands.CreateCategory;
using Electronic_Domain.Common.Enums;
using Electronic_Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Electronic_Application.Features.Order.Commands.CreateOrder
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Guid>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IShippingDetailRepository _shippingDetailRepository;
        private readonly IMapper _mapper;
        // private readonly IEmailService _emailService;
        private readonly ILogger<CreateOrderCommand> _logger;

        public CreateOrderCommandHandler(IMapper mapper, IOrderRepository orderRepository,IShippingDetailRepository shippingDetailRepository, ILogger<CreateOrderCommand> logger)
        {
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
            _shippingDetailRepository = shippingDetailRepository ?? throw new ArgumentNullException(nameof(shippingDetailRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            //_emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public async Task<Guid> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            
            var mappedOrderEntity = _mapper.Map<Electronic_Domain.Entities.Order>(request);
            var mappedShippingEntity = _mapper.Map<ShippingDetail>(request.ShippingDetail);
          
                // Create a new order
                var orderEntity = new Electronic_Domain.Entities.Order()
                {                   
                    Total = mappedOrderEntity.Total,
                    OrderDate = DateTime.Now,
                    Status = OrderStatus.Pending,
                    OrderItems= mappedOrderEntity.OrderItems    // added order Items                
                };

                var submittedOrder = await _orderRepository.AddAsync(orderEntity);
                _logger.LogInformation($"Order {submittedOrder.Id} is successfully created.");

                mappedShippingEntity.OrderId = submittedOrder.Id;
                var submittedShippingDetailEntity = await _shippingDetailRepository.AddAsync(mappedShippingEntity);
                _logger.LogInformation($"ShippingDetail {submittedShippingDetailEntity.Id} is successfully created.");

                return submittedOrder.Id;       
        }
    }
}
