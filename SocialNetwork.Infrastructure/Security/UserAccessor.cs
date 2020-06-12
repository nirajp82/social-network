using Microsoft.AspNetCore.Http;
using SocialNetwork.Nucleus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace SocialNetwork.Infrastructure
{
    internal class UserAccessor : IUserAccessor
    {
        private readonly IHttpContextAccessor _contextAccessor;
        #region Constructor
        public UserAccessor(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }
        #endregion


        #region Public Methods
        public string GetCurrentUserName()
        {
            string userName = _contextAccessor.HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            return userName;
        }
        #endregion


        #region Private Methods
        #endregion
    }
}
