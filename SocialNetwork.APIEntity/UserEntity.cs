using System;
using System.Collections.Generic;
using System.Text;

namespace SocialNetwork.APIEntity
{
    public class UserEntity : BaseEntity
    {
        public string DisplayName { get; set; }
        public string UserName { get; set; }
        public string Token { get; set; }
        public string Image { get; set; }
    }
}
