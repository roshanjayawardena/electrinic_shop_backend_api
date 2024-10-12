using Electronic_Domain.Entities;

namespace Electronic_Application.Contracts.Persistence
{
    public interface IOrderRepository : IAsyncRepository<Order>
    {
    }
}
