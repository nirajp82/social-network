﻿using MediatR;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using SocialNetwork.Nucleus.Activity;
using SocialNetwork.Util;
using System;
using System.Net;
using System.Threading.Tasks;

namespace SocialNetwork.Infrastructure
{
    public class ValidateUnAttendanceFilter : IAsyncActionFilter
    {
        #region Member
        private IMediator _mediator { get; }
        private ILogger<ValidateUnAttendanceFilter> _logger { get; }
        #endregion


        #region Constructor
        public ValidateUnAttendanceFilter(IMediator mediator, ILogger<ValidateUnAttendanceFilter> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }
        #endregion


        #region Method        
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
        //            var param = context.ActionArguments.SingleOrDefault(p => p.Value is IEntity);
            if (context.ActionArguments["activityId"] is Guid activityId)
            {
                var response = await _mediator.Send(new UnAttendanceRequirement.Command { ActivityId = activityId });
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
