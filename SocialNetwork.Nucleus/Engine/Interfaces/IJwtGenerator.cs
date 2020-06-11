using System;
using System.Collections.Generic;
using System.Text;

namespace SocialNetwork.Nucleus
{
    public interface IJwtGenerator
    {
        string CreateToken(string userName);
    }
}
