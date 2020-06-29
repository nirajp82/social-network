using System;
using System.ComponentModel.DataAnnotations;

namespace SocialNetwork.DataModel
{
    public class Photo : IBaseModel
    {
        public Guid Id { get; set; }
        public bool IsMainPhoto { get; set; }
        
        [MaxLength(50)]
        public string ActualFileName { get; set; }
       
        [MaxLength(50)]
        public string CloudFileName { get; set; }

        [MaxLength(50)]
        public string ContentType { get; set; }
        public long Length { get; set; }
        public DateTime UploadedDate { get; set; }
        public Guid AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}