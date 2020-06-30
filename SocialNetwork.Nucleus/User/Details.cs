using MediatR;
using SocialNetwork.DataModel;
using SocialNetwork.Dto;
using SocialNetwork.EF.Repo;
using SocialNetwork.Util;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SocialNetwork.Nucleus.Engine.User
{
    public class Details
    {
        public class Query : IRequest<ProfileDto>
        {
            public Guid AppUserId { get; set; }
        }

        public class Handler : IRequestHandler<Query, ProfileDto>
        {
            #region Members
            private readonly IPhotoAccessor _photoAccessor;
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapperHelper _mapperHelper;
            #endregion


            #region Constructor
            public Handler(IPhotoAccessor photoAccessor, IUnitOfWork unitOfWork,
                IMapperHelper mapperHelper)
            {
                _photoAccessor = photoAccessor;
                _unitOfWork = unitOfWork;
                _mapperHelper = mapperHelper;
            }
            #endregion


            #region Methods
            public async Task<ProfileDto> Handle(Query request, CancellationToken cancellationToken)
            {
                AppUser appUser = await _unitOfWork.AppUserRepo.GetUserProfile(request.AppUserId);
                ProfileDto profile = _mapperHelper.Map<AppUser, ProfileDto>(appUser);
                _photoAccessor.PreparePhotosUrl(profile.Photos);
                _photoAccessor.PreparePhotoUrl(profile.MainPhoto);
                return profile;
            }
            #endregion
        }
    }
}
