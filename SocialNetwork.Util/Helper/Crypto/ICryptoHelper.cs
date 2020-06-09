namespace SocialNetwork.Util
{
    public interface ICryptoHelper
    {
        string CreateBase64Salt();
        string GenerateHash(string plainText, string base64Salt);
    }
}