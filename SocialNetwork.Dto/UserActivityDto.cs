using System;

namespace SocialNetwork.Dto
{
    public class UserActivityDto : BaseDto
    {
        public Guid ActivityId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Category { get; set; }

        public DateTime Date { get; set; }
    }
}
