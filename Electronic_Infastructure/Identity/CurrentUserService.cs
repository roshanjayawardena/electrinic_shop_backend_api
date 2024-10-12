using Electronic_Application.Contracts.Infastructure.Auth;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Electronic_Infastructure.Identity
{
    public class CurrentUserService :ICurrentUserService
    {
        private readonly HttpContext _httpContext;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContext = httpContextAccessor.HttpContext;
        }

        public bool IsAuthenticated => _httpContext.User.Identity.IsAuthenticated;

        public Guid UserId => IsAuthenticated && Guid.TryParse(_httpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier), out Guid _userId)
            ? _userId : Guid.Empty;

        public string Name => IsAuthenticated ?
            (_httpContext.User.Identity as ClaimsIdentity).Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name).Value
            : "anonymous";

        public string Email => _httpContext?.User?.FindFirstValue(ClaimTypes.Email);

        public IEnumerable<string> Roles
        {
            get
            {
                if (!IsAuthenticated)
                    return Enumerable.Empty<string>();

                IEnumerable<string> roles = (_httpContext.User.Identity as ClaimsIdentity)
                    .Claims.Where(c => c.Type == ClaimTypes.Role).Select(d => d.Value);
                return roles;
            }
        }

        public bool HasRole(params string[] roles) => roles.Any(role => _httpContext.User.IsInRole(role));
    }
}
