using SocialNetwork.DataModel;
using SocialNetwork.Dto;
using SocialNetwork.EF.Repo;
using SocialNetwork.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SocialNetwork.Nucleus
{
    internal class ProfileReader : IProfileReader
    {
        #region Members
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapperHelper _mapperHelper;
        private readonly IUserAccessor _userAccessor;
        #endregion


        #region Constructor
        public ProfileReader(IUnitOfWork unitOfWork, IMapperHelper mapperHelper, IUserAccessor userAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapperHelper = mapperHelper;
            _userAccessor = userAccessor;
        }
        #endregion


        #region Methods
        public async Task<ProfileDto> ReadProfile(Guid appUserId, CancellationToken cancellationToken)
        {
            AppUser appUser = await _unitOfWork.AppUserRepo.GetUserProfile(appUserId, cancellationToken);
            ProfileDto profileDto = _mapperHelper.Map<AppUser, ProfileDto>(appUser);

            profileDto.FollowersCount = await _unitOfWork.UserFollowerRepo.GetFollowersCountAsync(appUserId, cancellationToken);
            profileDto.FollowingCount = await _unitOfWork.UserFollowerRepo.GetFollowingCountAsync(appUserId, cancellationToken);
            profileDto.Following = await _unitOfWork.UserFollowerRepo
                                            .HasAnyAsync(f => f.Follower.Id == _userAccessor.GetCurrentUserId() &&
                                                                f.UserId == appUserId, cancellationToken);
            return profileDto;
        }
        #endregion
    }
}
