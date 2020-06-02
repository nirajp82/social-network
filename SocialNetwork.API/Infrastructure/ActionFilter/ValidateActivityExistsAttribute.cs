using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using SocialNetwork.Util;
using System;
using System.Net;
using System.Threading.Tasks;
using static SocialNetwork.Nucleus.Engine.Activities.Exists;

namespace SocialNetwork.API
{
    public class ValidateActivityExistsAttribute : TypeFilterAttribute
    {
        #region Members
        private class ValidateActivityExistsFilter : IAsyncActionFilter
        {
            #region Member
            private IMediator _mediator { get; }
            private ILogger<ValidateActivityExistsAttribute> _logger { get; }
            #endregion


            #region Constructor
             public ValidateActivityExistsFilter(IMediator mediator, ILogger<ValidateActivityExistsAttribute> logger)
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
                        //Check if record exists
                        if (await _mediator.Send(new Query() { Id = id.Value }))
                        {
                            await next();
                            return;
                        }
                        context.Result = new NotFoundObjectResult(id.Value);
                        return;
                    }
                }
                _logger.LogError("APIConst.ErrorMessages.InvalidParam");
                context.Result = new BadRequestObjectResult(APIConst.ErrorMessages.InvalidParam);
            }
            #endregion
        }
        #endregion


        #region Constuctor
        public ValidateActivityExistsAttribute() : base(typeof(ValidateActivityExistsFilter))
        {
        }
        #endregion
    }
}