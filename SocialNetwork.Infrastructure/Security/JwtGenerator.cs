using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SocialNetwork.Nucleus;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace SocialNetwork.Infrastructure
{
    internal class JwtGenerator : IJwtGenerator
    {
        #region Private Members
        private readonly IConfiguration _configuration;
        #endregion


        #region Constructor
        public JwtGenerator(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        #endregion


        #region Public Methods
        public string CreateToken(Guid appUserId, string userName)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.NameId, TODO: Encrypt Value appUserId.ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, userName)
            };

            //Generate Signing Credentials
            SymmetricSecurityKey securityKey = Helper.GenerateSecurityKey(_configuration);
            SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512Signature);

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = credentials
            };

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        #endregion
    }
}