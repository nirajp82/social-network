using MediatR;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using SocialNetwork.Nucleus.Engine.Activity;
using SocialNetwork.Util;
using System;
using System.Net;
using System.Threading.Tasks;

namespace SocialNetwork.Infrastructure
{
    public class ValidateAttendanceFilter : IAsyncActionFilter
    {
        #region Member
        private IMediator _mediator { get; }
        private ILogger<ValidateAttendanceFilter> _logger { get; }
        #endregion


        #region Constructor
        public ValidateAttendanceFilter(IMediator mediator, ILogger<ValidateAttendanceFilter> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }
        #endregion


        #region Method        
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (context.ActionArguments["activityId"] is Guid activityId)
            {
                var response = await _mediator.Send(new AttendanceRequirement.Command { ActivityId = activityId });
                if (response.Key)
                {
                    await next();
                    return;
                }
                throw new CustomException(HttpStatusCode.BadRequest, response.Value);
            }
            _logger.LogError("Missing activityId parameter!");
            throw new CustomException(HttpStatusCode.NotFound, new { MissingParameter = "Missing activityId parameter!" });
        }
        #endregion
    }
}
