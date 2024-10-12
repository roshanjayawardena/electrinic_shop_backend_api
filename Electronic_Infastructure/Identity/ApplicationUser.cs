using Electronic_Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Electronic_Infastructure.Identity
{
    public class ApplicationUser: IdentityUser
    {
        public Guid BusinessUserId { get; set; }
        public BusinessUser BusinessUser { get; set; }
    }
}
