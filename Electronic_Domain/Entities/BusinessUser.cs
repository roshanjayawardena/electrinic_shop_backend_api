using Electronic_Domain.Common;
using Electronic_Domain.Common.Enums;

namespace Electronic_Domain.Entities
{
    public class BusinessUser: EntityBase
    {
        public string Name { get; set; }
        public BusinessUserStatus Status { get; set; }
       
    }
}
