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

            Map<AppUser, UserDto>();
           
                      Map<AppUser, ProfileDto>(false)
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.IdentityUser != null ? src.IdentityUser.UserName : ""));

            Map<Engine.User.Edit.Command, AppUser>(false);

            Map<Engine.Photo.Add.Command, Photo>()
              .ForMember(dest => dest.ContentType, opt => opt.MapFrom(src => src.File.ContentType))
              .ForMember(dest => dest.ActualFileName, opt => opt.MapFrom(src => src.File.FileName))
              .ForMember(dest => dest.Length, opt => opt.MapFrom(src => src.File.Length));


            Map<Photo, PhotoDto>(false)
                .ForMember(dest => dest.Url, opt => opt.MapFrom<PhotoUrlResolver>());

            Map<DataModel.Comment, CommentDto>()
                .ForMember(dest => dest.UserDisplayName, opt => opt.MapFrom(src => src.Author.DisplayName))
                .ForMember(dest => dest.MainPhotoCloudFileName, opt => opt.MapFrom(src => src.Author.MainPhoto.CloudFileName))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Author.Id));
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
        private class PhotoUrlResolver : IValueResolver<Photo, PhotoDto, string>
        {
            private readonly IPhotoAccessor _photoAccessor;

            public PhotoUrlResolver(IPhotoAccessor photoAccessor)
            {
                _photoAccessor = photoAccessor;
            }

            public string Resolve(Photo source, PhotoDto destination, string destMember, ResolutionContext context)
            {
                return _photoAccessor.PreparePhotoUrl(source?.CloudFileName);
            }
        }
  
        #endregion
    }
}
