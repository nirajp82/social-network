using System;

namespace SocialNetwork.Dto
{
    public class UserDto : BaseDto
    {
        public Guid AppUserId { get; set; }
        public string DisplayName { get; set; }       
        public string UserName { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public string Image { get; set; }
    }
}
