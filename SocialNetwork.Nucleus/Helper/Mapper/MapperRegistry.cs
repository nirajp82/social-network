using AutoMapper;
using SocialNetwork.Dto;
using SocialNetwork.DataModel;
using SocialNetwork.Nucleus.Engine.Activity;
using SocialNetwork.Nucleus.Engine.Photo;

namespace SocialNetwork.Nucleus
{
    internal class MapperRegistry : Profile
    {
        #region Constructor
        public MapperRegistry()
        {
            Map<string, string>().ConvertUsing(str => string.IsNullOrWhiteSpace(str) ? str : str.Trim());
            Map<Value, ValueDto>();

            Map<Activity, ActivityDto>()
                .ForMember(dest => dest.Attendees, opt => opt.MapFrom(src => src.UserActivities));

            Map<UserActivity, AttendeeDto>(false)
                .ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.AppUser.DisplayName))
                .ForMember(dest => dest.Image, opt => opt.MapFrom<PhotoPropertyUrlResolver>());

            Map<AppUser, UserDto>();
            Map<Create.Command, Activity>();
            Map<Edit.Command, Activity>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ActivityId));

            Map<Add.Command, Photo>()
                .ForMember(dest => dest.ContentType, opt => opt.MapFrom(src => src.File.ContentType))
                .ForMember(dest => dest.ActualFileName, opt => opt.MapFrom(src => src.File.FileName))
                .ForMember(dest => dest.Length, opt => opt.MapFrom(src => src.File.Length));

            Map<AppUser, ProfileDto>(false)
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.IdentityUser != null ? src.IdentityUser.UserName : ""));

            Map<Engine.User.Edit.Command, AppUser>(false);
            Map<Photo, PhotoDto>(false);

            Map<Comment, CommentDto>()
                .ForMember(dest => dest.UserDisplayName, opt => opt.MapFrom(src => src.Author.DisplayName))
                .ForMember(dest => dest.UserImage, opt => opt.MapFrom(src => src.Author.MainPhoto.CloudFileName))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Author.IdentityUser.UserName));
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


        #region Property Resolver
        private class PhotoPropertyUrlResolver : IValueResolver<UserActivity, AttendeeDto, string>
        {
            private readonly IPhotoAccessor _photoAccessor;

            public PhotoPropertyUrlResolver(IPhotoAccessor photoAccessor)
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
