using System;
using System.Collections.Generic;
using System.Text;

namespace SocialNetwork.Infrastructure
{
    public interface IJwtGenerator
    {
        string CreateToken(string userName);
    }
}
