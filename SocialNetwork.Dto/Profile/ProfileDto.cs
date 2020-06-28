using System.Collections.Generic;

namespace SocialNetwork.Dto
{
    public class ProfileDto
    {
        public string DisplayName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Bio { get; set; }
        public PhotoDto MainPhoto { get; set; }

        //public bool IsFollowed { get; set; }
        //public int FollowersCount { get; set; }
        //public int FollowingCount { get; set; }
        public IEnumerable<PhotoDto> Photos { get; set; }
        public string Username { get; set; }
    }
}
