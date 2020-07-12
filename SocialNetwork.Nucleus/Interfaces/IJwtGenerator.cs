using System;

namespace SocialNetwork.Nucleus
{
    public interface IJwtGenerator
    {
        string CreateToken(Guid appUserId, string userName);
        string CreateRefreshToken();
    }
}
