using MediatR;
using Microsoft.AspNetCore.Http;
using SocialNetwork.DataModel;
using SocialNetwork.Dto;
using SocialNetwork.Dto.Profile;
using SocialNetwork.EF.Repo;
using SocialNetwork.Util;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SocialNetwork.Nucleus.Engine.Photo
{
    public class Details
    {
        public class Request : IRequest<ProfileDto>
        {
            public Guid AppUserId { get; set; }
        }

        public class Handler : IRequestHandler<Request, ProfileDto>
        {
            #region Members
            private readonly IPhotoAccessor _photoAccessor;
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapperHelper _mapperHelper;
            private readonly IUserAccessor _userAccessor;
            #endregion


            #region Constructor
            public Handler(IPhotoAccessor photoAccessor, IUnitOfWork unitOfWork,
                IMapperHelper mapperHelper, IUserAccessor userAccessor)
            {
                _photoAccessor = photoAccessor;
                _unitOfWork = unitOfWork;
                _mapperHelper = mapperHelper;
                _userAccessor = userAccessor;
            }
            #endregion


            #region Methods
            public async Task<ProfileDto> Handle(Request request, CancellationToken cancellationToken)
            {
                AppUser appUser = await _unitOfWork.AppUserRepo.GetUserProfile(request.AppUserId);
                ProfileDto profile = _mapperHelper.Map<AppUser, ProfileDto>(appUser);
                return profile;
            }
            #endregion
        }
    }
}
