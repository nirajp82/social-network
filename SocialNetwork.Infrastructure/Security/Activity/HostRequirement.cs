using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;
using SocialNetwork.Nucleus;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Diagnostics;
using System.Linq;
using MediatR;
using SocialNetwork.Nucleus.Engine.Activity;

namespace SocialNetwork.Infrastructure
{
    public class IsHostRequirement : IAuthorizationRequirement
    {
    }

    public class IsHostRequirementHandler : AuthorizationHandler<IsHostRequirement>
    {
        #region Members
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMediator _mediator;
        #endregion


        #region Constuctor
        public IsHostRequirementHandler(IHttpContextAccessor httpContextAccessor, IMediator mediator)
        {
            _httpContextAccessor = httpContextAccessor;
            _mediator = mediator;
        }
        #endregion


        #region Constuctor
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, IsHostRequirement requirement)
        {
            Guid activityId = Guid.Parse(_httpContextAccessor.HttpContext.Request.RouteValues.SingleOrDefault(x => x.Key == "activityId").Value.ToString());
            bool isHost = await _mediator.Send(new IsHost.Query { ActivityId = activityId });
            if (isHost)
                context.Succeed(requirement);
        }
        #endregion
    }
}