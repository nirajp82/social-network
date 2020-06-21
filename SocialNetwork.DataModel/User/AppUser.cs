using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SocialNetwork.DataModel
{
    public class AppUser : AuditModel
    {
        public Guid Id { get; set; }

        [MaxLength(24)]
        public string FirstName { get; set; }

        [MaxLength(24)]
        public string LastName { get; set; }

        [MaxLength(24)]
        public string Email { get; set; }

        [MaxLength(240)]
        public string Bio { get; set; }

        public IdentityUser IdentityUser { get; set; }

        public IEnumerable<UserActivity> Activities { get; set; }

        public IEnumerable<Photo> Photos { get; set; }
    }
}
