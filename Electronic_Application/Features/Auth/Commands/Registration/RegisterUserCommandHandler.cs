using AutoMapper;
using Electronic_Application.Contracts.Infastructure.Auth;
using Electronic_Application.Features.Auth.Commands.Login;
using Electronic_Application.Models.Auth;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace Electronic_Application.Features.Auth.Commands.Registration
{
    public class RegisterUserCommandHandler:  IRequestHandler<RegisterUserCommand, bool>
    {
        private readonly IAuthService _authService;       
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        public RegisterUserCommandHandler(IAuthService authService, IConfiguration configuration, IMapper mapper) { 
        
            _authService = authService;
            _configuration= configuration;
            _mapper = mapper;
        }

        public async Task<bool> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var registerUserModel = _mapper.Map<RegisterUserModel>(request);
            var result = await _authService.RegisterUser(registerUserModel);

            if (result)
            {
                //var registerDesignerEmail = new RegisterDesignerSendEmailModel
                //{
                //    NotificationType = TopicNotificationType.RegisterDesigner,
                //    DesignerEmail = request.Email,
                //    DesignerName = string.Concat(request.FirstName, " ", request.LastName)
                //};
                //await _messageSenderService.SendMessagestoServiceBusAsync(JsonConvert.SerializeObject(registerDesignerEmail), _configuration.GetSection("NotificationTopic").Value);
                return true;
            }

            return false;
        }
    }
}
