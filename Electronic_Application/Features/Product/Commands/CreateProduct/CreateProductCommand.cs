using MediatR;
using Microsoft.AspNetCore.Http;

namespace Electronic_Application.Features.Product.Commands.CreateProduct
{
    public class CreateProductCommand: IRequest<Guid>
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public Guid CategoryId { get; set; }
        public string Description { get; set; }
        public string Brand { get; set; }
        //public string Image { get; set; }
        public IFormFile Image { get; set; }
    }
}
