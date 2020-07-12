using Microsoft.AspNetCore.Authorization;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Linq;
using MediatR;
using SocialNetwork.Nucleus.Activity;

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
            //Mark handler as success by calling context.Succeed, passing the requirement that has been successfully validated.
            if (isHost)
                context.Succeed(requirement);
        }
        #endregion
    }
}