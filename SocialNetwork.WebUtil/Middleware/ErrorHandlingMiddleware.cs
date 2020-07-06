using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
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
            try
            {
                await _next(httpContext);
            }
            catch (CustomException cex)
            {
                object errors = cex.Errors;
                await HandleException(cex, httpContext, errors, (int)cex.StatusCode);
            }
            catch (SecurityTokenExpiredException ste)
            {
                httpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                httpContext.Response.Headers.Add("www-authenticate", "invalid_token");
                await HandleException(ste, httpContext, new { TokenExpired = ste.Message }, (int)HttpStatusCode.Unauthorized);
            }
            catch (Exception ex)
            {
                object errors = string.IsNullOrWhiteSpace(ex.Message) ? ex.Message : "Oops, something went wrong";
                await HandleException(ex, httpContext, errors, (int)HttpStatusCode.InternalServerError);
            }
        }
        #endregion


        #region Private Methods
        private async Task HandleException(Exception ex, HttpContext httpContext, object errors, int statusCode)
        {
            //Note: Please be careful when logging request body, it may contain sensitive information.
            _logger.LogError(ex, $"Reuest Body:{await RequestUtil.GetBodyAsync(httpContext.Request)}");
            httpContext.Response.StatusCode = statusCode;
            if (errors != null)
            {
                var errorBody = JsonSerializer.Serialize(new { errors });
                await httpContext.Response.WriteAsync(errorBody);
            }
        }
        #endregion
    }
}