using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SocialNetwork.DataModel
{
    public class Activity : BaseModel
    {
        public Guid Id { get; set; }

        [MaxLength(50)]
        public string Title { get; set; }

        [MaxLength(150)]
        public string Description { get; set; }

        [MaxLength(50)]
        public string Category { get; set; }

        public DateTime Date { get; set; }

        [MaxLength(50)]
        public string City { get; set; }

        [MaxLength(50)]
        public string Venue { get; set; }
    }
}
