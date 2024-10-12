using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Electronic_Application.Behaviours
{
    public class UnhandledExceptionBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly ILogger<TRequest> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UnhandledExceptionBehaviour(ILogger<TRequest> logger,IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }


        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            try
            {
                return await next();
            }
            catch (Exception ex)
            {

                var requestName = typeof(TRequest).Name;
                _logger.LogError(ex, $"Electronic Commerce Request: Unhandled Exception for Request {requestName} {JsonConvert.SerializeObject(request)}");

                throw;    
            }
        }   
    }
}
