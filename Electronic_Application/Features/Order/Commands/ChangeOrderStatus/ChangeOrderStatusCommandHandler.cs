using AutoMapper;
using Electronic_Application.Contracts.Infastructure;
using Electronic_Application.Contracts.Infastructure.Auth;
using Electronic_Application.Exceptions;
using Electronic_Domain.Common.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Electronic_Application.Features.Order.Commands.ChangeOrderStatus
{
    public class ChangeOrderStatusCommandHandler : IRequestHandler<ChangeOrderStatusCommand, bool>
    {
        private readonly IApplicationDBContext _dbContext;
        private readonly IMapper _mapper;      
        private readonly ILogger<ChangeOrderStatusCommand> _logger;      

        public ChangeOrderStatusCommandHandler(IApplicationDBContext dBContext, IMapper mapper, ILogger<ChangeOrderStatusCommand> logger)
        {
            _dbContext = dBContext ?? throw new ArgumentNullException(nameof(dBContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));          
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public async Task<bool> Handle(ChangeOrderStatusCommand request, CancellationToken cancellationToken)
        {
            var orderEntity = await _dbContext.Orders.FirstOrDefaultAsync(w=>w.Id == request.OrderId);
            if (orderEntity == null)
            {
                throw new NotFoundException(nameof(Electronic_Domain.Entities.Order), request.OrderId);
            }
            if (orderEntity != null) {
                 orderEntity.Status = (OrderStatus)request.Status;
                 await _dbContext.SaveChangesAsync(cancellationToken);
                _logger.LogInformation($"Order status has successfully updated.");
                 return true;
            }
            return false;
        }
    }
}
