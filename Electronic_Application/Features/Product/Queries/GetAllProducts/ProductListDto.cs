namespace Electronic_Application.Features.Product.Queries.GetAllProducts
{
    public class ProductListDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public string Brand { get; set; }
        public string Category { get; set; }
        public Guid CategoryId { get; set; }
        public string Image { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
