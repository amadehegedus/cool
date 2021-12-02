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

        public RequestContext(IHttpContextAccessor httpContext)
        {
            var context = httpContext.HttpContext;

            UserName = context.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            Name = context.User.Claims.First(c => c.Type == ClaimTypes.Name).Value;
            Email = context.User.Claims.First(c => c.Type == ClaimTypes.Email).Value;
            Role = Enum.Parse<Role>(context.User.Claims.First(c => c.Type == ClaimTypes.Role).Value);
        }
    }
}
