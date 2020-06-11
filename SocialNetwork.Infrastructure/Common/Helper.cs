using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace SocialNetwork.Infrastructure
{
    internal class Helper
    {
        #region Public Members
        public static SymmetricSecurityKey GenerateSecurityKey(IConfiguration configuration)
        {
            //Read from UserSecrets file, in Development environment, For rest of the environment use Environment Variable, Cloud Secret service.

            //Note: During Development, If using IIS, Please place secrets file somewhere within the API folder.
            //If api using IIS Express, It can be created using using VS studio and it will be placed somewhere inside %APPDATA% folder.
            //https://stackoverflow.com/questions/49597408/asp-net-core-2-web-application-isnt-loading-user-secrets-when-debugging-iis-web
            string securityKey = configuration["Security:Key"];//social-network
            if (!string.IsNullOrWhiteSpace(securityKey))
            {
                return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));
            }
            throw new Exception("Missing Security Key");
        }
        #endregion
    }
}
