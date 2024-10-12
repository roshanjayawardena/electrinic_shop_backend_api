using MediatR;

namespace Electronic_Application.Features.Product.Queries.GetProductById
{
    public class GetProductByIdQuery : IRequest<ProductDto>
    {
        public Guid Id { get; set; }
    }
}
