using AutoMapper;
using SocialNetwork.Dto;
using SocialNetwork.DataModel;
using SocialNetwork.Nucleus.Engine.Activity;

namespace SocialNetwork.Nucleus
{
    internal class ActivityMapper : BaseMapper
    {
        #region Constructor
        public ActivityMapper()
        {
            Map<Activity, ActivityDto>()
               .ForMember(dest => dest.Attendees, opt => opt.MapFrom(src => src.UserActivities));

            Map<UserActivity, AttendeeDto>(false)
                .ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.AppUser.DisplayName))               
                .ForMember(dest => dest.Image, opt => opt.MapFrom<AttendeePhotoUrlResolver>());

            Map<Create.Command, Activity>();
            Map<Edit.Command, Activity>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ActivityId));

            Map<Activity, UserActivityDto>(false)
                .ForMember(dest => dest.ActivityId, opt => opt.MapFrom(src => src.Id));
        }
        #endregion


        #region Resolver
        private class AttendeePhotoUrlResolver : IValueResolver<UserActivity, AttendeeDto, string>
        {
            private readonly IPhotoAccessor _photoAccessor;

            public AttendeePhotoUrlResolver(IPhotoAccessor photoAccessor)
            {
                _photoAccessor = photoAccessor;
            }

            public string Resolve(UserActivity source, AttendeeDto destination, string destMember, ResolutionContext context)
            {
                return _photoAccessor.PreparePhotoUrl(source?.AppUser?.MainPhoto?.CloudFileName);
            }
        }       
        #endregion
    }
}
