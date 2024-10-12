using Electronic_Domain.Common;

namespace Electronic_Domain.Entities
{
    public class Product:EntityBase
    {
        public string Name { get; set; }
        public double Price { get; set; }       
        public string Description { get; set; }
        public string Brand { get; set; }
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
        public string Image { get; set; }
        public bool IsDeleted { get; set; }
    }
}
