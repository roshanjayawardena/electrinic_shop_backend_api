using AutoMapper;
using Electronic_Application.Contracts.Infastructure.Auth;
using Electronic_Application.Exceptions;
using Electronic_Application.Features.Auth.Commands.Login;
using Electronic_Application.Models.Auth;
using Electronic_Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Electronic_Application.Features.Auth.Commands.RefreshToken
{
    public class RefreshTokenCommandHandler: IRequestHandler<RefreshTokenCommand, LoginDto>
    {
        private readonly IMapper _mapper;
        // private readonly IEmailService _emailService;
        private readonly IAuthService _authService;
        private readonly ILogger<RefreshTokenCommand> _logger;

        public RefreshTokenCommandHandler(IAuthService authService, IMapper mapper, ILogger<RefreshTokenCommand> logger)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            //_emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
            _authService = authService ?? throw new ArgumentNullException(nameof(authService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public async Task<LoginDto> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var refreshTokenModel = _mapper.Map<RefreshTokenDto>(request);
            var tokens = await _authService.RenewTokens(refreshTokenModel);

            if (tokens == null)
            {
                throw new ValidationException("Refresh Token","Invalid Refresh Token");
            }
            return tokens;
        }
    }
}
