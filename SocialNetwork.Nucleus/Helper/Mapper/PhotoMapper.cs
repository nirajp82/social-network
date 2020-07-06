using AutoMapper;
using SocialNetwork.Dto;
using SocialNetwork.DataModel;

namespace SocialNetwork.Nucleus
{
    internal class PhotoMapper : BaseMapper
    {
        #region Constructor
        public PhotoMapper()
        {
            Map<Engine.Photo.Add.Command, Photo>()
            .ForMember(dest => dest.ContentType, opt => opt.MapFrom(src => src.File.ContentType))
            .ForMember(dest => dest.ActualFileName, opt => opt.MapFrom(src => src.File.FileName))
            .ForMember(dest => dest.Length, opt => opt.MapFrom(src => src.File.Length));

            Map<Photo, PhotoDto>(false)
                .ForMember(dest => dest.Url, opt => opt.MapFrom<PhotoUrlResolver>());
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
