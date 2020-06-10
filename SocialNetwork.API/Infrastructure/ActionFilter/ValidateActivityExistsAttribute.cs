using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using SocialNetwork.Util;
using System;
using System.Net;
using System.Threading.Tasks;
using static SocialNetwork.Nucleus.Engine.Activity.Exists;

namespace SocialNetwork.API
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
            if (context.ActionArguments.ContainsKey("id"))
            {
                Guid? id = context.ActionArguments["id"] as Guid?;
                if (id.HasValue)
                {
                    //Check if record exists, Send Request Exists
                    if (await _mediator.Send(new ExistsQuery() { Id = id.Value }))
                    {
                        await next();
                        return;
                    }
                    throw new CustomException(HttpStatusCode.NotFound, new { MissingParameter = "Missing id parameter!" });
                }
            }
            throw new CustomException(HttpStatusCode.BadRequest, new { MissingParameter = "Missing id parameter!" });
        }
        #endregion
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
}