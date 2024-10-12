using MediatR;

namespace Electronic_Application.Features.Order.Commands.ChangeOrderStatus
{
    public class ChangeOrderStatusCommand: IRequest<bool>
    {
        public Guid OrderId { get; set; }
        public int Status { get; set; }
    }
}
