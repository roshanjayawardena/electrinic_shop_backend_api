using Electronic_Application.Contracts.Persistence;
using Electronic_Domain.Entities;
using Electronic_Infastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Electronic_Infastructure.Repositories
{
    public class ProductRepository: RepositoryBase<Product>, IProductRepository
    {
        public ProductRepository(ElectronicContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<Product>> GetAllProducts()
        {
            //var productList =  _dbContext.Products.Where(p => !p.IsDeleted).Include(w => w.Category)
            //   .Select(w => new ProductListDto()
            //   {
            //       Id = w.Id,
            //       Name = w.Name,
            //       Price = w.Price,
            //       Description = w.Description,
            //       Brand = w.Brand,
            //       Category = w.Category.Name,
            //       CategoryId = w.CategoryId,
            //       Image = w.Image,
            //       CreatedDate = w.CreatedDate
            //   }).OrderByDescending(a => a.CreatedDate).AsNoTracking().AsQueryable();

            var productList = await _dbContext.Products.Where(p => !p.IsDeleted).Include(w => w.Category).OrderByDescending(a => a.CreatedDate).ToListAsync();
            //.Select(w => new ProductListDto()
            //{
            //    Id = w.Id,
            //    Name = w.Name,
            //    Price = w.Price,
            //    Description = w.Description,
            //    Brand = w.Brand,
            //    Category = w.Category.Name,
            //    CategoryId = w.CategoryId,
            //    Image = w.Image,
            //    CreatedDate = w.CreatedDate
            //})
            return productList;
           
        }

        public async Task<Product> GetProductById(Guid productId)
        {
            var product = await _dbContext.Products.Include(w => w.Category).FirstOrDefaultAsync(p => p.Id == productId && !p.IsDeleted);           
            return product;
        }            
    }
}
