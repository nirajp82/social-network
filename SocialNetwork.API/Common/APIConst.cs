using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetwork.API
{
    internal class APIConst
    {
        internal class StatusCodes
        {
            internal const int Status499ClientClosedRequest = 499;
        }

        internal class ErrorMessages 
        {
            internal const string InvalidParam = "InvalidParam";
        }

        internal const string LineSeparator = "\n";        
    }
}
