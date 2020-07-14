using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SocialNetwork.Nucleus;
using SocialNetwork.Util;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace SocialNetwork.Infrastructure
{
    internal class JwtGenerator : IJwtGenerator
    {
        #region Private Members
        private readonly IConfiguration _configuration;
        private readonly ICryptoHelper _cryptoHelper;
        private readonly ConfigSettings _configSettings;
        #endregion


        #region Constructor
        public JwtGenerator(IConfiguration configuration, ICryptoHelper cryptoHelper, ConfigSettings configSettings)
        {
            _configuration = configuration;
            _cryptoHelper = cryptoHelper;
            _configSettings = configSettings;
        }
        #endregion


        #region Public Methods
        public string CreateToken(Guid appUserId, string userName)
        {
            var claims = new List<Claim>
            {
                new Claim(Constants.CLAIM_UID, _cryptoHelper.Encrypt(_configSettings.DataProtectionKey,appUserId)),
                new Claim(Constants.CLAIM_UNAME, _cryptoHelper.Encrypt(_configSettings.DataProtectionKey,userName))
            };

            //Generate Signing Credentials
            SymmetricSecurityKey securityKey = Helper.GenerateSecurityKey(_configuration);
            SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512Signature);
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                NotBefore = DateTime.Now,
                Expires = DateTime.Now.AddSeconds(Constants.TOKEN_EXPIRES_IN),
                SigningCredentials = credentials,
            };

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public string CreateRefreshToken()
        {
            byte[] randomValueHolder = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomValueHolder);
            return Convert.ToBase64String(randomValueHolder);
        }
        #endregion
    }
}