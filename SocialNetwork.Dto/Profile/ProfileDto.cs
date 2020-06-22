using System;
using System.Collections.Generic;
using System.Text;

namespace SocialNetwork.Dto
{
    public class ProfileDto
    {
        public string DisplayName { get; set; }
        public string Username { get; set; }
        public string Image { get; set; }
        public string Bio { get; set; }
        //public bool IsFollowed { get; set; }
        //public int FollowersCount { get; set; }
        //public int FollowingCount { get; set; }
        public IEnumerable<string> Photos { get; set; }
    }
}
