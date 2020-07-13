using AutoMapper;
using SocialNetwork.Dto;
using SocialNetwork.Nucleus.Comment;
using SocialNetwork.Util;
using System;

namespace SocialNetwork.Nucleus
{
    internal class CommentMapper : BaseMapper
    {
        #region Constructor
        public CommentMapper()
        {
            Map<DataModel.Comment, CommentDto>()
            .ForMember(dest => dest.UserDisplayName, opt => opt.MapFrom(src => src.Author.DisplayName))
            .ForMember(dest => dest.UserImage, opt => opt.MapFrom<CommentPhotoUrlResolver>())
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Author.Id));

            Map<Create.Command, DataModel.Comment>(false)
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.CreatedDate, opt => HelperFunc.GetCurrentDateTime());
        }
        #endregion

        #region Resolver
        private class CommentPhotoUrlResolver : IValueResolver<DataModel.Comment, CommentDto, string>
        {
            private readonly IPhotoAccessor _photoAccessor;

            public CommentPhotoUrlResolver(IPhotoAccessor photoAccessor)
            {
                _photoAccessor = photoAccessor;
            }

            public string Resolve(DataModel.Comment source, CommentDto dest, string destMember, ResolutionContext context)
            {
                return _photoAccessor.PreparePhotoUrl(source.Author.MainPhoto.CloudFileName);
            }
        }
        #endregion
    }
}
