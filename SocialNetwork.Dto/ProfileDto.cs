using System;
using System.Collections.Generic;

namespace SocialNetwork.Dto
{
    public class ProfileDto
    {
        public Guid AppUserId { get; set; }
        public string DisplayName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Bio { get; set; }
        public PhotoDto MainPhoto { get; set; }
        public bool Following { get; set; }
        public long FollowersCount { get; set; }
        public long FollowingCount { get; set; }
        public IEnumerable<PhotoDto> Photos { get; set; }
        public string Username { get; set; }
    }
}
