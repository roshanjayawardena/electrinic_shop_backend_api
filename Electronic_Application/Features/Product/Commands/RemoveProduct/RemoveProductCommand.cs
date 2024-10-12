using MediatR;

namespace Electronic_Application.Features.Product.Commands.RemoveProduct
{
    public class RemoveProductCommand: IRequest<bool>
    {
        public Guid Id { get; set; }
    }
}
