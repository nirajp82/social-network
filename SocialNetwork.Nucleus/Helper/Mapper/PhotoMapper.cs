using AutoMapper;
using SocialNetwork.Dto;

namespace SocialNetwork.Nucleus
{
    internal class PhotoMapper : BaseMapper
    {
        #region Constructor
        public PhotoMapper()
        {
            Map<Photo.Add.Command, DataModel.Photo>()
            .ForMember(dest => dest.ContentType, opt => opt.MapFrom(src => src.File.ContentType))
            .ForMember(dest => dest.ActualFileName, opt => opt.MapFrom(src => src.File.FileName))
            .ForMember(dest => dest.Length, opt => opt.MapFrom(src => src.File.Length));

            Map<DataModel.Photo, PhotoDto>(false)
                .ForMember(dest => dest.Url, opt => opt.MapFrom<PhotoUrlResolver>());
        }
        #endregion


        #region Property Resolver
        private class PhotoUrlResolver : IValueResolver<DataModel.Photo, PhotoDto, string>
        {
            private readonly IPhotoAccessor _photoAccessor;

            public PhotoUrlResolver(IPhotoAccessor photoAccessor)
            {
                _photoAccessor = photoAccessor;
            }

            public string Resolve(DataModel.Photo source, PhotoDto destination, string destMember, ResolutionContext context)
            {
                return _photoAccessor.PreparePhotoUrl(source?.CloudFileName);
            }
        }
        #endregion
    }
}
