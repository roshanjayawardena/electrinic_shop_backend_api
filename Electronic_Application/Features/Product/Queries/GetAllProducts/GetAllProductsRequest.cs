using MediatR;

namespace Electronic_Application.Features.Product.Queries.GetAllProducts
{
    public class GetAllProductsRequest : IRequest<List<ProductListDto>>
    {
    }
}
