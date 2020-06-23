using System;

namespace SocialNetwork.Dto
{
    public class AttendeeDto : BaseDto
    {
        public Guid AppUserId { get; set; }
        public string DisplayName { get; set; }
        public string MainPhoto { get; set; }
        public bool IsHost { get; set; }
    }
}
