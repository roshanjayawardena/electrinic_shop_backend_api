using AutoMapper;
using Electronic_Application.Contracts.Infastructure.Auth;
using Electronic_Application.Models.Auth;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Electronic_Application.Features.Auth.Commands.Login
{

    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, LoginDto>
    {
        
        private readonly IMapper _mapper;
        // private readonly IEmailService _emailService;
        private readonly IAuthService _authService;
        private readonly ILogger<LoginUserCommand> _logger;

        public LoginUserCommandHandler(IAuthService authService, IMapper mapper, ILogger<LoginUserCommand> logger)
        {            
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            //_emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
            _authService = authService ?? throw new ArgumentNullException(nameof(authService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public async Task<LoginDto> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var loginModel = _mapper.Map<LoginUser>(request);
            return await _authService.Login(loginModel);            
        }
    }
}
