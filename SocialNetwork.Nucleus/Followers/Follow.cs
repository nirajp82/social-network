using MediatR;
using SocialNetwork.DataModel;
using SocialNetwork.Dto;
using SocialNetwork.EF.Repo;
using SocialNetwork.Util;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace SocialNetwork.Nucleus.Followers
{
    public class Follow
    {
        public class Command : IRequest<ProfileDto>
        {
            public Guid FollowingUserId { get; set; }
        }

        public class Handler : IRequestHandler<Command, ProfileDto>
        {
            #region Members
            private readonly IUnitOfWork _unitOfWork;
            private readonly IUserAccessor _userAccessor;
            private readonly IProfileReader _profileReader;
            #endregion


            #region Constuctor
            public Handler(IUnitOfWork unitOfWork, IUserAccessor userAccessor, IProfileReader profileReader)
            {
                _unitOfWork = unitOfWork;
                _userAccessor = userAccessor;
                _profileReader = profileReader;
            }
            #endregion


            #region Methods
            public async Task<ProfileDto> Handle(Command request, CancellationToken cancellationToken)
            {
                Guid appUserId = _userAccessor.GetCurrentUserId();

                await Validate(appUserId, request, cancellationToken);

                UserFollower userFollower = new UserFollower
                {
                    UserId = request.FollowingUserId,
                    FollowerId = appUserId
                };
                _unitOfWork.UserFollowerRepo.Add(userFollower);

                int insertCnt = await _unitOfWork.SaveAsync(cancellationToken);
                if (insertCnt > 0)
                    return await _profileReader.ReadProfile(appUserId, cancellationToken);

                throw new Exception("Problem saving changes to database");
            }

            private async Task Validate(Guid appUserId, Command request, CancellationToken cancellationToken)
            {
                if (request.FollowingUserId == appUserId)
                    throw new CustomException(HttpStatusCode.BadRequest, new { User = "You can not follow yourself" });

                AppUser followingUser = await _unitOfWork.AppUserRepo.FindFirstAsync(u => u.Id == request.FollowingUserId, null, cancellationToken);
                if (followingUser == null)
                    throw new CustomException(HttpStatusCode.NotFound, new { User = "Not Found" });

                bool isFollowing = await _unitOfWork.UserFollowerRepo.HasAnyAsync(u => u.FollowerId == appUserId && u.UserId == followingUser.Id, cancellationToken);
                if (isFollowing)
                    throw new CustomException(HttpStatusCode.BadRequest, new { User = "You are already following this user" });
            }
            #endregion
        }
    }
}
