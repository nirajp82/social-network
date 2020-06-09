using System;
using System.ComponentModel.DataAnnotations;

namespace SocialNetwork.DataModel
{
    public interface IAuditModel : IBaseModel
    {
        public DateTime CreatedDate { get; set; }

        public string CreatedBy { get; set; }

        public DateTime UpdatedDate { get; set; }

        public string UpdatedBy { get; set; }
    }

    public class AuditModel : IAuditModel
    {
        public DateTime CreatedDate { get; set; }

        [MaxLength(24)]
        public string CreatedBy { get; set; }

        public DateTime UpdatedDate { get; set; }

        [MaxLength(24)]
        public string UpdatedBy { get; set; }
    }
}
