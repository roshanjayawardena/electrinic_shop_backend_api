using Electronic_Application.Contracts.Persistence;
using Electronic_Domain.Entities;
using Electronic_Infastructure.Persistence;

namespace Electronic_Infastructure.Repositories
{
    public class CategoryRepository: RepositoryBase<Category>, ICategoryRepository
    {
        public CategoryRepository(ElectronicContext dbContext) : base(dbContext)
        {
        }
    }
}
