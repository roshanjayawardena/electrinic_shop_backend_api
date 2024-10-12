using Electronic_Application.Features.Category.Commands.CreateCategory;
using Electronic_Application.Features.Category.Queries.GetAllCategories;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Electronic_Commerce.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoryController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet(Name = "GetCategories")]
        [ProducesResponseType(typeof(IEnumerable<CategoryDto>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<List<CategoryDto>>> GetAllCategories()
        {
            var categories = await _mediator.Send(new GetCategoryListRequest() { });
            return Ok(categories);
        }

        [HttpPost(Name = "CreateCategory")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<int>> CreateCategory([FromBody] CreateCategoryCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
