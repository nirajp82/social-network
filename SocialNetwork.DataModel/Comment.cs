using System;
using System.ComponentModel.DataAnnotations;

namespace SocialNetwork.DataModel
{
    public class Comment : IBaseModel
    {
        public Guid Id { get; set; }

        [MaxLength(240)]
        public string Body { get; set; }
        public Guid AuthorId { get; set; }
        public AppUser Author { get; set; }
        public Guid ActivityId { get; set; }
        public Activity Activity { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
