using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace SocialNetwork.Infrastructure
{
    internal class JWTTokenHelper
    {
        #region Public Methods
        internal static TokenValidationParameters InitTokenValidationParameters(IConfiguration configuration, bool validateLifetime)
        {
            TokenValidationParameters tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = Helper.GenerateSecurityKey(configuration),
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateLifetime = validateLifetime,
                ClockSkew = TimeSpan.Zero
            };
            return tokenValidationParameters;
        }

        internal static JwtBearerEvents InitJwtBearerEvents()
        {
            JwtBearerEvents jwtBearerEvents = new JwtBearerEvents
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
                OnAuthenticationFailed = (context) =>
                {
                    ////For expired token to stop the execution throw an exception. (Without it continues with requests)
                    if (context.Exception is SecurityTokenExpiredException)
                    {
                        //throw context.Exception;
                    }
                    return Task.CompletedTask;
                },
                OnTokenValidated = (context) =>
                {
                    DateTime validFrom = context.SecurityToken.ValidFrom;
                    DateTime ValidTo = context.SecurityToken.ValidTo;
                    return Task.CompletedTask;
                },
            };
            return jwtBearerEvents;
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
