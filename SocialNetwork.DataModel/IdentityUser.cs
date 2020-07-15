using System;
using System.ComponentModel.DataAnnotations;

namespace SocialNetwork.DataModel
{
    public class IdentityUser : AuditModel
    {
        public Guid Id { get; set; }

        [MaxLength(24)]
        public string UserName { get; set; }

        [MaxLength(50)]
        public string Passoword { get; set; }

        [MaxLength(50)]
        public string Salt { get; set; }

        [MaxLength(50)]
        public string RefreshToken { get; set; }

        public DateTime? RefreshTokenExpiry { get; set; }

        [MaxLength(50)]
        public string PreviousRefreshToken { get; set; }

        public DateTime? PreviousRefreshTokenExpiry { get; set; }

        public Guid AppUserId { get; set; }

        public AppUser AppUser { get; set; }
    }
}
