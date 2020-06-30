using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace SocialNetwork.Dto
{
    public class CommentDto
    {
        public Guid Id { get; set; }
        public string Body { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UserDisplayName { get; set; }
        public string UserImage { get; set; }
        public Guid UserId { get; set; }

        [JsonIgnore]
        public string MainPhotoCloudFileName { get; set; }
    }
}
