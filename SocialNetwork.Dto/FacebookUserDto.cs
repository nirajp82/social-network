using System.Text.Json.Serialization;

namespace SocialNetwork.Dto
{
    public class FacebookUserDto
    {
        public class FacebookUserInfo
        {
            public string Id { get; set; }
            public string Name { get; set; }
            public string Email { get; set; }
            [JsonPropertyName("first_name")]
            public string FirstName { get; set; }
            public string Username { get; set; }
            public FacebookPictureDataDto Picture { get; set; }
        }

        public class FacebookPictureDataDto
        {
            public FacebookPictureDto Data { get; set; }
        }

        public class FacebookPictureDto
        {
            public string Url { get; set; }
        }
    }
}
