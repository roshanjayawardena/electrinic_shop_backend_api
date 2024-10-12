using Electronic_Application.Contracts.Infastructure;
using Electronic_Application.Contracts.Infastructure.Auth;
using Electronic_Application.Features.Order.Commands.ChangeOrderStatus;
using Electronic_Application.Features.Order.Commands.CreateOrder;
using Electronic_Application.Features.Order.Queries.GetAllOrders;
using Electronic_Application.Features.Order.Queries.GetOrderById;
using Electronic_Application.Features.Order.Queries.GetOrdersByUserId;
using Electronic_Application.Features.Product.Queries.GetProductById;
using Electronic_Application.Models.Common;
using Electronic_Domain.Common.Enums;
using Electronic_Infastructure.SignalRHub;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Net;


namespace Electronic_Commerce.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/[controller]")]
    [ApiController]   
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;      
        private readonly ICurrentUserService _currentUserService;
        public OrderController(IMediator mediator, ICurrentUserService currentUserService) {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));            
            _currentUserService = currentUserService;
        }

        [HttpPost(Name = "CreateOrder")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<int>> CreateOrder([FromBody] CreateOrderCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet(Name = "GetAllOrders")]       
        public async Task<ActionResult<PaginationResponse<List<OrderListDto>>>> GetAllOrders([FromQuery] GetAllOrdersRequest searchedOrderQuery)
        {
            var result = await _mediator.Send(searchedOrderQuery);
            return Ok(result);
        }

        [HttpGet("GetAllOrdersByUserId")]
        public async Task<ActionResult<PaginationResponse<List<OrderListByUserIdDto>>>> GetAllOrdersByUserId([FromQuery] GetOrdersByUserIdQuery searchedOrderQuery)
        {
            var result = await _mediator.Send(searchedOrderQuery);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IList<ProductDto>>> GetOrderItemsById(Guid id)
        {
            var result = await _mediator.Send(new GetOrderItemsByIdQuery() { Id = id});
            return Ok(result);
        }

        [HttpPut(Name = "ChanngeStatus")]        
        public async Task<ActionResult<bool>> ChanngeStatus([FromBody] ChangeOrderStatusCommand command)
        {
            var userId = _currentUserService.UserId;
            var result = await _mediator.Send(command);           
            return Ok(result);
        }
    }
}
