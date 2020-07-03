using System;

namespace SocialNetwork.Nucleus
{
    public interface IUserAccessor
    {
        Guid GetCurrentUserId();
        string GetCurrentUserName();
    }
}
