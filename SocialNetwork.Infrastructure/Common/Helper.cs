using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace SocialNetwork.Infrastructure
{
    internal class Helper
    {
        #region Private Members
        private static SymmetricSecurityKey _securityKey;
        #endregion

        #region Internal Members
        internal static SymmetricSecurityKey SecurityKey => _securityKey ??
                (_securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("8AD97805E10E4FB78D1B800DB295F177"))); 
        #endregion
    }
}
