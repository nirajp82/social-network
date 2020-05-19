using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace SocialNetwork.API
{
    public class TaskCanceledExceptionFilter : ExceptionFilterAttribute
    {
        #region Members
        public ILogger<TaskCanceledExceptionFilter> _logger { get; }
        #endregion


        #region Constuctor
        public TaskCanceledExceptionFilter(ILogger<TaskCanceledExceptionFilter> logger)
        {
            _logger = logger;
        }
        #endregion


        #region Public Methods
        public override void OnException(ExceptionContext context)
        {
            if (context.Exception is OperationCanceledException)
            {
                _logger.LogInformation("Request was cancelled");
                context.ExceptionHandled = true;
                context.Result = new StatusCodeResult(APIConst.StatusCodes.Status499ClientClosedRequest);
            }
        }
        #endregion
    }
}