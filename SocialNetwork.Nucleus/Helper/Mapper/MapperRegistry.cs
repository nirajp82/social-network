using AutoMapper;
using SocialNetwork.Dto;
using SocialNetwork.DataModel;
using SocialNetwork.Nucleus.Engine.Activity;
using SocialNetwork.Nucleus.Engine.Photo;
using System.Linq;
using System;

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
                .ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => $"{src.AppUser.LastName}, {src.AppUser.FirstName}"))
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
                .ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => $"{src.LastName}, {src.FirstName}"))
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.IdentityUser != null ? src.IdentityUser.UserName : ""))
                .ForMember(dest => dest.Photos, opt => opt.MapFrom(src => src.Photos != null ? src.Photos.Select(p => p.CloudFileName) : null))
                .ForMember(dest => dest.MainPhoto, opt => opt.MapFrom(src => src.Photos != null ? src.Photos.Where(p => p.IsMainPhoto).Select(p => p.CloudFileName).FirstOrDefault() : null));

            Map<Photo, PhotoDto>(false);
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
