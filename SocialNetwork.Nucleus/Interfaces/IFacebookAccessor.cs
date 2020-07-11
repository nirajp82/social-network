using SocialNetwork.Dto;
using System;
using System.Threading.Tasks;

namespace SocialNetwork.Nucleus
{
    public interface IFacebookAccessor
    {
        Task<FacebookUserDto> FacebookLogin(string accessToken);
    }
}
