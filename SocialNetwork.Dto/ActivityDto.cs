using System;
using System.Collections.Generic;

namespace SocialNetwork.Dto
{
    public class ActivityDto : BaseDto
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Category { get; set; }

        public DateTime Date { get; set; }

        public string City { get; set; }

        public string Venue { get; set; }

        public IEnumerable<AttendeeDto> Attendees { get; set; }

        public IEnumerable<CommentDto> Comments { get; set; }
    }
}
