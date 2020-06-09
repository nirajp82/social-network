using System;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace SocialNetwork.Util
{
    public class CryptoHelper : ICryptoHelper
    {
        public string CreateBase64Salt()
        {
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            return Convert.ToBase64String(salt);
        }

        public string GenerateHash(string plainText, string base64Salt)
        {
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: plainText,
            salt: Convert.FromBase64String(base64Salt),
            prf: KeyDerivationPrf.HMACSHA512,
            iterationCount: 10000,
            numBytesRequested: 256 / 8));

            return hashed;
        }
    }
}
