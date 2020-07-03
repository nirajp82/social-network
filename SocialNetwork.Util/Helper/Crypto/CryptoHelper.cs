using System;
using System.ComponentModel;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace SocialNetwork.Util
{
    public class CryptoHelper : ICryptoHelper
    {
        #region Methods
        public T Decrypt<T>(string keyString, string cipherText)
        {
            if (string.IsNullOrWhiteSpace(keyString))
                throw new ArgumentException(nameof(keyString));

            if (string.IsNullOrWhiteSpace(cipherText))
                throw new ArgumentNullException(nameof(cipherText));

            byte[] fullCipher = Convert.FromBase64String(cipherText);

            var iv = new byte[16];
            var cipher = new byte[16];

            Buffer.BlockCopy(fullCipher, 0, iv, 0, iv.Length);
            Buffer.BlockCopy(fullCipher, iv.Length, cipher, 0, iv.Length);
            var key = Encoding.UTF8.GetBytes(keyString);

            using (Aes aesAlg = Aes.Create())
            {
                using (ICryptoTransform decryptor = aesAlg.CreateDecryptor(key, iv))
                {
                    string result;
                    using (MemoryStream msDecrypt = new MemoryStream(cipher))
                    {
                        using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                        {
                            using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                                result = srDecrypt.ReadToEnd();
                        }
                    }

                    return HelperFunc.ChangeType<T>(result);
                }
            }
        }

        public string Encrypt<T>(string keyString, T value)
        {
            if (string.IsNullOrWhiteSpace(keyString))
                throw new ArgumentException(nameof(keyString));

            string plainText = HelperFunc.ChangeType<string>(value);
            if (string.IsNullOrWhiteSpace(plainText))
                throw new ArgumentNullException(nameof(value));

            // Create an Aes object with the specified key
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Encoding.UTF8.GetBytes(keyString);

                // Create an encryptor to perform the stream transform.
                using (ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV))
                {
                    // Create the streams used for encryption.
                    using (MemoryStream msEncrypt = new MemoryStream())
                    {
                        using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                        {
                            using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                            {
                                //Write all data to the stream.
                                swEncrypt.Write(plainText);
                            }
                            byte[] encryptedContent = msEncrypt.ToArray();
                            byte[] result = new byte[aesAlg.IV.Length + encryptedContent.Length];
                            Buffer.BlockCopy(aesAlg.IV, 0, result, 0, aesAlg.IV.Length);
                            Buffer.BlockCopy(encryptedContent, 0, result, aesAlg.IV.Length, encryptedContent.Length);
                            return Convert.ToBase64String(result);
                        }
                    }
                }
            }
        }

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
        #endregion
    }
}
