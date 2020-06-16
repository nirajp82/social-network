using System;
using System.Collections.Generic;
using System.Text;

namespace SocialNetwork.DTO
{
    public class AttendeeDTO : BaseDTO
    {
        public string DisplayName { get; set; }
        public string Image { get; set; }
        public bool IsHost { get; set; }
    }
}
