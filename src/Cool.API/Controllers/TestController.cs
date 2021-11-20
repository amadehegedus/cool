using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cool.Api.Authentication;
using Cool.Common.Enums;
using Cool.Common.RequestContext;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cool.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [Authorize(AuthenticationSchemes = AuthenticationSchemes.JwtBearer, Roles = nameof(Role.User))]
    //[Authorize(AuthenticationSchemes = AuthenticationSchemes.JwtBearer, Roles = nameof(Role.User) + "," + nameof(Role.Admin))]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly IRequestContext _context;

        public TestController(IRequestContext context)
        {
            _context = context;
        }

        [HttpGet]
        public string HelloWorld()
        {
            return $"Hello {_context.UserName} {_context.Name} {_context.Email} {_context.Role}!";
        }
    }
}
