using SocialNetwork.Dto;
using SocialNetwork.DataModel;

namespace SocialNetwork.Nucleus
{
    internal class UserMapper : BaseMapper
    {
        #region Constructor
        public UserMapper()
        {
            Map<Value, ValueDto>();
            Map<AppUser, UserDto>();

            Map<AppUser, ProfileDto>(false)
                .ForMember(dest => dest.AppUserId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.IdentityUser != null ? src.IdentityUser.UserName : ""));

            Map<Engine.User.Edit.Command, AppUser>(false);
        }
        #endregion

    }
}
