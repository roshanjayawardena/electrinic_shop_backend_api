using Electronic_Application.Features.Product.Commands.CreateProduct;
using Electronic_Application.Features.Product.Commands.RemoveProduct;
using Electronic_Application.Features.Product.Commands.UpdateProduct;
using Electronic_Application.Features.Product.Queries.GetAllProducts;
using Electronic_Application.Features.Product.Queries.GetProductById;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using ProductDto = Electronic_Application.Features.Product.Queries.GetProductById.ProductDto;

namespace Electronic_Commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]  
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet(Name = "GetProducts")]
        [ProducesResponseType(typeof(IEnumerable<ProductListDto>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<List<ProductListDto>>> GetProducts()
        {
            var products = await _mediator.Send(new GetAllProductsRequest() { });
            return Ok(products);
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost(Name = "CreateProduct")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<int>> CreateProduct([FromForm] CreateProductCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPut(Name = "UpdateProduct")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<int>> UpdateProduct([FromForm] UpdateProductCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet("{id}")]       
        public async Task<ActionResult<ProductDto>> GetProductById(Guid id)
        {
            var result = await _mediator.Send(new GetProductByIdQuery() { Id = id });
            return Ok(result);
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<ProductDto>> RemoveProduct(Guid id)
        {
            var result = await _mediator.Send(new RemoveProductCommand() { Id = id });
            return Ok(result);
        }

    }
}
