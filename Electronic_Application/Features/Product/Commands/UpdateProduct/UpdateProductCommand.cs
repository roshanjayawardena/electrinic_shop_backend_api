using MediatR;
using Microsoft.AspNetCore.Http;

namespace Electronic_Application.Features.Product.Commands.UpdateProduct
{
    public class UpdateProductCommand: IRequest<Guid>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public Guid CategoryId { get; set; }
        public string Description { get; set; }
        public string Brand { get; set; }
        public IFormFile? Image { get; set; }
    }
}
