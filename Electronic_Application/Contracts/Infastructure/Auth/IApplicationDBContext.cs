using Electronic_Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Electronic_Application.Contracts.Infastructure.Auth
{
    public interface IApplicationDBContext
    {
        DbSet<Category> Categories { get; set; }
        DbSet<Product> Products { get; set; }
        DbSet<Order> Orders { get; set; }
        DbSet<OrderItem> OrderItems { get; set; }
        DbSet<ShippingDetail> ShippingDetails { get; set; }
        DbSet<RefreshToken> RefreshToken { get; set; }
        DbSet<BusinessUser> BusinessUser { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
