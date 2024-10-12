using Electronic_Application.Contracts.Persistence;
using Electronic_Domain.Entities;
using Electronic_Infastructure.Persistence;

namespace Electronic_Infastructure.Repositories
{
    public class ShippingDetailRepository : RepositoryBase<ShippingDetail>, IShippingDetailRepository
    {
        public ShippingDetailRepository(ElectronicContext dbContext) : base(dbContext)
        {
        }
    }
}
