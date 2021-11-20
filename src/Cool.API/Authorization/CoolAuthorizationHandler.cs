using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cool.Common.Enums;
using Cool.Common.RequestContext;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace Cool.Api.Authorization
{
    public class CoolAuthorizationHandler : AuthorizationHandler<RolesAuthorizationRequirement>
    {
        private readonly IRequestContext _requestContext;

        public CoolAuthorizationHandler(IRequestContext requestContext)
        {
            _requestContext = requestContext;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, RolesAuthorizationRequirement requirement)
        {
            var user = context.User;

            if (user == null || !user.Identity.IsAuthenticated)
            {
                context.Fail();
                return;
            }

            var userRole = _requestContext.Role;

            //If the current user's role is not in the required roles given in the controller, fail
            if (requirement.AllowedRoles.All(role => Enum.Parse<Role>(role) != userRole))
            {
                context.Fail();
                return;
            }

            context.Succeed(requirement);
        }
    }
}
