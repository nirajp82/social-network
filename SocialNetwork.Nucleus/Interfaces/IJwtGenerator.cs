namespace SocialNetwork.Nucleus
{
    public interface IJwtGenerator
    {
        string CreateToken(string userName);
    }
}
