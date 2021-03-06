﻿using Microsoft.AspNetCore.Http;
using SocialNetwork.Nucleus;
using SocialNetwork.Util;
using System;
using System.Linq;

namespace SocialNetwork.Infrastructure
{
    internal class UserAccessor : IUserAccessor
    {
        #region Members
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly ICryptoHelper _cryptoHelper;
        private readonly ConfigSettings _configSettings;
        #endregion


        #region Constructor
        public UserAccessor(IHttpContextAccessor contextAccessor, ICryptoHelper cryptoHelper, ConfigSettings configSettings)
        {
            _contextAccessor = contextAccessor;
            _cryptoHelper = cryptoHelper;
            _configSettings = configSettings;
        }
        #endregion


        #region Public Methods
        public Guid GetCurrentUserId()
        {
            string encryptedUserId = FindClaim(Constants.CLAIM_UID);
            return _cryptoHelper.Decrypt<Guid>(_configSettings.DataProtectionKey, encryptedUserId);
        }

        public string GetCurrentUserName()
        {
            string encryptedUserName = FindClaim(Constants.CLAIM_UNAME);

            if (!string.IsNullOrWhiteSpace(encryptedUserName))
                return _cryptoHelper.Decrypt<string>(_configSettings.DataProtectionKey, encryptedUserName);

            return string.Empty;
        }
        #endregion


        #region Private Methods
        private string FindClaim(string claimName)
        {
            return _contextAccessor.HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == claimName)?.Value;
        }
        #endregion
    }
}
