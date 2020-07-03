namespace SocialNetwork.Util
{
    public interface ICryptoHelper
    {
        string CreateBase64Salt();

        string GenerateHash(string plainText, string base64Salt);

        string Encrypt<T>(string keyString, T plainText);

        T Decrypt<T>(string keyString, string cipherText);
    }
}