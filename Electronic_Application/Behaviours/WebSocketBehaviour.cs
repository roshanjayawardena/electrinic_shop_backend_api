using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Net.Http;

namespace Electronic_Application.Behaviours
{

    public class WebSocketBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public WebSocketBehaviour(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            //var request = _httpContextAccessor.HttpContext.Request;

            // web sockets cannot pass headers so we must take the access token from query param and
            // add it to the header before authentication middleware runs
            if (_httpContextAccessor.HttpContext.Request.Path.StartsWithSegments("/hub", StringComparison.OrdinalIgnoreCase) &&
                _httpContextAccessor.HttpContext.Request.Query.TryGetValue("access_token", out var accessToken))
            {
                _httpContextAccessor.HttpContext.Request.Headers.Add("Authorization", $"Bearer {accessToken}");
            }

            return await next();
        }
    }
}
