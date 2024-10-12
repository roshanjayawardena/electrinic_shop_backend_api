using Electronic_Application.Features.Product.Queries.GetAllProducts;
using Electronic_Domain.Entities;

namespace Electronic_Application.Contracts.Persistence
{
    public interface IProductRepository : IAsyncRepository<Product>
    {
        //Task<IQueryable<ProductListDto>> GetAllProducts();
        Task<List<Product>> GetAllProducts();
        Task<Product> GetProductById(Guid productId);
    }
}
