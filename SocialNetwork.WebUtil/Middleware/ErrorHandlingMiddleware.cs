using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using SocialNetwork.Util;
using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace SocialNetwork.WebUtil
{
    public class ErrorHandlingMiddleware
    {
        #region Members
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;
        #endregion


        #region Constructor
        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }
        #endregion

        #region Public Methods
        public async Task InvokeAsync(HttpContext httpContext)
        {
            string requestBody = await RequestUtil.GetBodyAsync(httpContext.Request);
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }
        #endregion


        #region Private Methods
        private async Task HandleExceptionAsync(HttpContext httpContext, Exception ex)
        {
            object errors = null;
            switch (ex)
            {
                case CustomException cex:
                    errors = cex.Errors;
                    httpContext.Response.StatusCode = (int)cex.StatusCode;
                    break;
                default:
                    errors = string.IsNullOrWhiteSpace(ex.Message) ? ex.Message : "Oops, something went wrong";
                    httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }

            //Note: Please be careful when logging request body, it may contain sensitive information.
            _logger.LogError(ex, $"Body:{await RequestUtil.GetBodyAsync(httpContext.Request)}");

            httpContext.Response.ContentType = "";
            if (errors != null)
            {
                var errorBody = JsonSerializer.Serialize(new { errors });
                await httpContext.Response.WriteAsync(errorBody);
            }
        }
        #endregion
    }
}