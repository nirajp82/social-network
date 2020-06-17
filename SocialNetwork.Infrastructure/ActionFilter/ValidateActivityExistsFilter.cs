using MediatR;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using SocialNetwork.Util;
using System;
using System.Net;
using System.Threading.Tasks;
using static SocialNetwork.Nucleus.Engine.Activity.Exists;

namespace SocialNetwork.Infrastructure
{
    public class ValidateActivityExistsFilter : IAsyncActionFilter
    {
        #region Member
        private IMediator _mediator { get; }
        private ILogger<ValidateActivityExistsFilter> _logger { get; }
        #endregion


        #region Constructor
        public ValidateActivityExistsFilter(IMediator mediator, ILogger<ValidateActivityExistsFilter> logger)
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
                //Check if record exists, Send Request Exists
                if (await _mediator.Send(new Query() { ActivityId = activityId }))
                {
                    await next();
                    return;
                }
            }
            _logger.LogError("Missing activityId parameter!");
            throw new CustomException(HttpStatusCode.NotFound, new { MissingParameter = "Missing activityId parameter!" });
            #endregion
        }
    }
}

//public class ValidateActivityExistsAttribute : TypeFilterAttribute
//{
//    #region Members
//    #endregion

//    #region Constuctor
//    public ValidateActivityExistsAttribute() : base(typeof(ValidateActivityExistsFilter))
//    {
//    }
//    #endregion
//}