namespace SocialNetwork.Dto
{
    public class AttendeeDto : BaseDto
    {
        public string DisplayName { get; set; }
        public string Image { get; set; }
        public bool IsHost { get; set; }
    }
}
