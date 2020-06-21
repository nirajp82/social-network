using AutoMapper;
using SocialNetwork.Dto;
using SocialNetwork.DataModel;
using SocialNetwork.Nucleus.Engine.Activity;
using SocialNetwork.Nucleus.Engine.Photo;
using SocialNetwork.Dto.Profile;
using System.Linq;

namespace SocialNetwork.Nucleus.Helper
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
                .ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => $"{src.AppUser.LastName}, {src.AppUser.FirstName}"));

            Map<AppUser, UserDto>();
            Map<Create.Command, Activity>();
            Map<Edit.Command, Activity>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ActivityId));

            Map<Add.Command, Photo>()
                .ForMember(dest => dest.ContentType, opt => opt.MapFrom(src => src.File.ContentType))
                .ForMember(dest => dest.ActualFileName, opt => opt.MapFrom(src => src.File.FileName))
                .ForMember(dest => dest.IsMainPhoto, opt => opt.MapFrom(src => src.IsMainPhoto))
                .ForMember(dest => dest.Length, opt => opt.MapFrom(src => src.File.Length));

            Map<AppUser, ProfileDto>(false)
                .ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => $"{src.LastName}, {src.FirstName}"))
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Photos.FirstOrDefault(p => p.IsMainPhoto)))
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.IdentityUser.UserName));

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
    }
}
