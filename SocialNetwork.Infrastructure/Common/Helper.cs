using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SocialNetwork.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocialNetwork.Infrastructure
{
    internal class Helper
    {
        #region Public Members
        public static SymmetricSecurityKey GenerateSecurityKey(IConfiguration configuration)
        {
            /*Read from UserSecrets file, in Development environment, For rest of the environment use Environment Variable, Cloud Secret service.

            Note: During Development, If using IIS, Please place secrets file somewhere within the API folder.
            If api using IIS Express, It can be created using using VS studio and it will be placed somewhere inside % APPDATA % folder.
            https://stackoverflow.com/questions/49597408/asp-net-core-2-web-application-isnt-loading-user-secrets-when-debugging-iis-web */

            string securityKey = configuration[Constants.SECURITY_TOKEN_KEY_NAME];//social-network

            if (!string.IsNullOrWhiteSpace(securityKey))
                return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));

            throw new ArgumentException("Invalid Security Key");
        }

        public static T FindRouteValue<T>(IHttpContextAccessor _httpContextAccessor, string routeKey)
        {
            RouteValueDictionary routeValues = _httpContextAccessor.HttpContext.Request.RouteValues;
            KeyValuePair<string, object> routeValuePair = routeValues.SingleOrDefault(x => HelperFunc.IsEqualString(x.Key, routeKey));
            return HelperFunc.ChangeType<T>(routeValuePair.Value);
        }
        #endregion
    }
}
