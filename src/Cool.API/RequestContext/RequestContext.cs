using System;
using System.Linq;
using System.Security.Claims;
using Cool.Common.Enums;
using Cool.Common.RequestContext;
using Microsoft.AspNetCore.Http;

namespace Cool.Api.RequestContext
{
    public class RequestContext : IRequestContext
    {
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public Role Role { get; set; }

        private readonly HttpContext _httpContext;

        public RequestContext(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext.HttpContext;

            UserName = _httpContext.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            Name = _httpContext.User.Claims.First(c => c.Type == ClaimTypes.Name).Value;
            Email = _httpContext.User.Claims.First(c => c.Type == ClaimTypes.Email).Value;
            Role = Enum.Parse<Role>(_httpContext.User.Claims.First(c => c.Type == ClaimTypes.Role).Value);
        }
    }
}
