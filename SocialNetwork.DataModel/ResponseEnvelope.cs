using System;
using System.Collections.Generic;
using System.Text;

namespace SocialNetwork.DataModel
{
    public class ResponseEnvelope<T>
    {
        public IEnumerable<T> List { get; set; }
        public int Count { get; set; }
    }
}
