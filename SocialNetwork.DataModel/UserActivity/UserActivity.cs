using System;
using System.ComponentModel.DataAnnotations;

namespace SocialNetwork.DataModel
{
    public class UserActivity : IBaseModel
    {
        public Guid AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public Guid ActivityId { get; set; }
        public Activity Activity { get; set; }
        public DateTime DateJoined { get; set; }
        public bool IsHost { get; set; }
    }
}
