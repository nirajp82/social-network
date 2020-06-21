using System;
using System.Collections.Generic;
using System.Text;

namespace SocialNetwork.Dto.Profile
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
        public ICollection<PhotoDto> Photos { get; set; }
    }
}
