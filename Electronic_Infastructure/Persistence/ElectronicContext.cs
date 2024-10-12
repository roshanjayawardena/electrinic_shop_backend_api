using Electronic_Application.Contracts.Infastructure.Auth;
using Electronic_Domain.Common;
using Electronic_Domain.Entities;
using Electronic_Infastructure.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Electronic_Infastructure.Persistence
{
    public class ElectronicContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>, IApplicationDBContext
    {
        private readonly ICurrentUserService _currentUserService;
        public ElectronicContext(DbContextOptions<ElectronicContext> options, ICurrentUserService currentUserService) : base(options)
        {
            _currentUserService = currentUserService;
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<ShippingDetail> ShippingDetails { get; set; }
        public DbSet<BusinessUser> BusinessUser { get; set; }
        public DbSet<RefreshToken> RefreshToken { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<EntityBase>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedBy = _currentUserService.UserId;
                        entry.Entity.LastModifiedBy = _currentUserService.UserId;                     
                        entry.Entity.CreatedDate = DateTime.Now;
                        entry.Entity.LastModifiedDate = DateTime.Now;
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModifiedBy = _currentUserService.UserId;
                        entry.Entity.LastModifiedDate = DateTime.Now;
                        break;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
     
     }
}
