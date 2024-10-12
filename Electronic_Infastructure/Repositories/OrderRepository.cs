using Electronic_Application.Contracts.Persistence;
using Electronic_Domain.Entities;
using Electronic_Infastructure.Persistence;

namespace Electronic_Infastructure.Repositories
{
    public class OrderRepository : RepositoryBase<Order>, IOrderRepository
    {
        public OrderRepository(ElectronicContext dbContext) : base(dbContext)
        {
        }
    }
}
