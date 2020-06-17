namespace SocialNetwork.Dto
{
    public class UserDto : BaseDto
    {
        public string DisplayName { get; set; }
        public string UserName { get; set; }
        public string Token { get; set; }
        public string Image { get; set; }
    }
}
