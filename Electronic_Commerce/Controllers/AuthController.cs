using Electronic_Application.Exceptions;
using Electronic_Application.Features.Auth.Commands.Login;
using Electronic_Application.Features.Auth.Commands.RefreshToken;
using Electronic_Application.Features.Auth.Commands.Registration;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Electronic_Commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]    
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(LoginUserCommand command)
        {           
          var result = await _mediator.Send(command);
          return Ok(result);        
        }

        [HttpPost("register")]
        public async Task<ActionResult<bool>> Register(RegisterUserCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost("refresh-token")]
        public async Task<ActionResult<bool>> RefreshToken(RefreshTokenCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
