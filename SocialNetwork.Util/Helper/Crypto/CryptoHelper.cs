﻿using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace SocialNetwork.Util
{

    public class CryptoHelper : ICryptoHelper
    {
        #region Methods
         /// <summary>
    /// Decrypt given Encrypted Content (Cipher Text)
    /// </summary>
    /// <param name="keyString">32 character long key that will be used to decrypt the content</param>
    /// <param name="iv">Initialization vector - That will be used to create Encryptor - 16 character long </param>
    /// <param name="cipherText">data that needs to be decrypted</param>
    /// <returns>Plain string</returns>
        public T Decrypt<T>(string keyString, string cipherText)
        {
            if (string.IsNullOrWhiteSpace(keyString))
                throw new ArgumentNullException(nameof(keyString));

            if (string.IsNullOrWhiteSpace(cipherText))
                throw new ArgumentNullException(nameof(cipherText));

            byte[] fullCipher = Convert.FromBase64String(cipherText);

            var iv = new byte[16];
            var cipher = new byte[fullCipher.Length - iv.Length];

            Buffer.BlockCopy(fullCipher, 0, iv, 0, iv.Length);
            Buffer.BlockCopy(fullCipher, iv.Length, cipher, 0, fullCipher.Length - iv.Length);
            var key = Encoding.UTF8.GetBytes(keyString);

            // Create an Aes object with the specified key
            using Aes aesAlg = Aes.Create();
            aesAlg.Key = Encoding.UTF8.GetBytes(keyString);

            // Create a decryptor to perform the stream transform.
            using ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, iv);
            string plainText;
            // Create the streams used for decryption.
            using (MemoryStream msDecrypt = new MemoryStream(cipher))
            {
                using CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read);
                // Read the decrypted bytes from the decrypting stream
                // and place them in a string.
                using StreamReader srDecrypt = new StreamReader(csDecrypt);
                plainText = srDecrypt.ReadToEnd();
            }
            //Return Plain Text
            return HelperFunc.ChangeType<T>(plainText);
        }

 /// <summary>
    /// Encrypt data using AES algorithm
    /// </summary>
    /// <param name="keyString">key that will be used to encrypt the content - 32 character long</param>
    /// <param name="iv">Initialization vector - That will be used to create Encryptor - 16 character long </param>
    /// <param name="value">data that needs to be encrypted</param>
    /// <returns>Encrypted Content (Cipher Text) string</returns>
        public string Encrypt<T>(string keyString, T value)
        {
            if (string.IsNullOrWhiteSpace(keyString))
                throw new ArgumentNullException(nameof(keyString));

            string plainText = HelperFunc.ChangeType<string>(value);
            if (string.IsNullOrWhiteSpace(plainText))
                throw new ArgumentNullException(nameof(value));

            // Create an Aes object with the specified key
            using Aes aesAlg = Aes.Create();
            aesAlg.Key = Encoding.UTF8.GetBytes(keyString);

            // Create an encryptor to perform the stream transform.
            using ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
            // Create the streams used for encryption.
            using MemoryStream msEncrypt = new MemoryStream();
            using CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write);
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
        
        public void GenerateAESKeyIv()
        {
         using (var aesAlg = Aes.Create())
      {
        string key = Convert.ToBase64String(aesAlg.Key);
        string iv = Convert.ToBase64String(aesAlg.IV);

      }
        }
        #endregion
    }
}
