using Microsoft.AspNetCore.DataProtection.XmlEncryption;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SocialNetwork.Nucleus;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;

namespace SocialNetwork.Infrastructure
{
    internal class UserAccessor : IUserAccessor
    {
        #region Members
        private readonly IHttpContextAccessor _contextAccessor;
        #endregion


        #region Constructor
        public UserAccessor(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }
        #endregion


        #region Public Methods
        public Guid GetCurrentUserId()
        {
            string userId = _contextAccessor.HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.UniqueName)?.Value;
            //TODO: DpapiNGXmlDecryptor User Id
            return new Guid(userId);
        }


        public string GetCurrentUserName()
        {
            string userName = _contextAccessor.HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.UniqueName)?.Value;
            return userName;
        }
        #endregion


        #region Private Methods
        #endregion
    }
}
