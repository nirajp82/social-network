using System;
using System.Text.Json.Serialization;

namespace SocialNetwork.Dto
{
    public class PhotoDto
    {
        public Guid Id { get; set; }
        public string Url { get; set; }
        [JsonIgnore]
        public string CloudFileName { get; set; }        
    }
}
