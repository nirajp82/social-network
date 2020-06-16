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

        //public Guid IdentityUserId { get; set; }

        public IdentityUser IdentityUser { get; set; }

        public IEnumerable<UserActivity> UserActivities { get; set; }
    }
}
