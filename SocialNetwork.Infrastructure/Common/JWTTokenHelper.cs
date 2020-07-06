using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Azure;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Text.Json;

namespace SocialNetwork.Infrastructure
{
    internal class JWTTokenHelper
    {
        #region Public Methods
        internal static void InitJwtBearerOptions(JwtBearerOptions opt, IConfiguration configuration)
        {
            opt.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = Helper.GenerateSecurityKey(configuration),
                //TODO: Customize this options.
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateLifetime = true,
                //LifetimeValidator = CustomLifetimeValidator,
                ClockSkew = TimeSpan.Zero,
            };

            opt.Events = new JwtBearerEvents
            {
                OnMessageReceived = (context) =>
                {
                    //Set Access Token for Chat Request.
                    var accessToken = context.Request.Query[Constants.ACCESS_TOKEN];
                    var path = context.HttpContext.Request.Path;
                    if (!string.IsNullOrWhiteSpace(accessToken) &&
                         path.StartsWithSegments(Constants.ACTIVITY_CHAT_HUB))
                    {
                        context.Token = accessToken;
                    }
                    return Task.CompletedTask;
                },
                //OnTokenValidated = (context) =>
                //{
                //    DateTime validFrom = context.SecurityToken.ValidFrom;
                //    DateTime ValidTo = context.SecurityToken.ValidTo;
                //    return Task.CompletedTask;
                //},
                OnAuthenticationFailed = (context) =>
                {
                    ////For expired token to stop the execution throw an exception. (Without it continues with requests)
                    if (context.Exception is SecurityTokenExpiredException)
                    {
                        throw context.Exception;
                    }
                    return Task.CompletedTask;
                }
            };
        }
        #endregion


        #region Private Methods
        private static bool CustomLifetimeValidator(DateTime? notBefore, DateTime? expires,
                    SecurityToken tokenToValidate, TokenValidationParameters @param)
        {
            if (expires != null)
            {
                return expires > DateTime.UtcNow;
            }
            return false;
        }
        #endregion
    }
}
