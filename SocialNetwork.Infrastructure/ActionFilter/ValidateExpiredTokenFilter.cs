using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using SocialNetwork.Nucleus.User;
using SocialNetwork.Util;
using System;
using System.Linq;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace SocialNetwork.Infrastructure
{
    public class ValidateExpiredTokenFilter : IAsyncActionFilter
    {
        #region Member
        private readonly IConfiguration _configuration;
        private readonly ILogger<ValidateUnAttendanceFilter> _logger;
        private readonly ICryptoHelper _cryptoHelper;
        private readonly ConfigSettings _configSettings;
        #endregion


        #region Constructor
        public ValidateExpiredTokenFilter(IConfiguration configuration, ILogger<ValidateUnAttendanceFilter> logger,
            ICryptoHelper cryptoHelper, ConfigSettings configSettings)
        {
            _configuration = configuration;
            _cryptoHelper = cryptoHelper;
            _configSettings = configSettings;
            _logger = logger;
        }
        #endregion


        #region Method        
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (context.ActionArguments["query"] is RefreshToken.Query refreshTokenQuery)
            {
                TokenValidationParameters tokenValidationParameters = JWTTokenHelper.InitTokenValidationParameters(_configuration, false);
                var tokenHandler = new JwtSecurityTokenHandler();
                ClaimsPrincipal principal = tokenHandler.ValidateToken(refreshTokenQuery.Token, tokenValidationParameters, out var securityToken);

                if (securityToken is JwtSecurityToken jwtSecurityToken &&
                    jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha512, StringComparison.InvariantCultureIgnoreCase))
                {
                    string encryptedUserName = principal.Claims.FirstOrDefault(c => c.Type == Constants.CLAIM_UNAME)?.Value;
                    refreshTokenQuery.UserName = _cryptoHelper.Decrypt<string>(_configSettings.DataProtectionKey, encryptedUserName);
                    await next();
                    return;
                }
                throw new SecurityTokenException("Invalid Token");
            }
            _logger.LogError("Missing query parameter!");
            throw new CustomException(HttpStatusCode.NotFound, new { MissingParameter = "Missing query parameter!" });
        }
        #endregion
    }
}
