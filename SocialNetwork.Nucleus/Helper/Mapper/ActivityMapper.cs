using AutoMapper;
using SocialNetwork.Dto;
using SocialNetwork.DataModel;
using SocialNetwork.Nucleus.Engine.Activity;
using SocialNetwork.Util;
using SocialNetwork.EF.Repo;

namespace SocialNetwork.Nucleus
{
    internal class ActivityMapper : Profile
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

        }
        #endregion


        #region Private Methods
        IMappingExpression<source, dest> Map<source, dest>(bool createReserseMap = true)
        {
            if (createReserseMap)
                return CreateMap<dest, source>().ReverseMap();

            return CreateMap<source, dest>();
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
