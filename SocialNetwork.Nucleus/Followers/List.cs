using MediatR;
using SocialNetwork.DataModel;
using SocialNetwork.Dto;
using SocialNetwork.EF.Repo;
using SocialNetwork.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SocialNetwork.Nucleus.Followers
{
    public class List
    {
        public class Query : IRequest<IEnumerable<ProfileDto>>
        {
            public Guid AppUserId { get; set; }

            public string Predicate { get; set; }
        }

        public class Handler : IRequestHandler<Query, IEnumerable<ProfileDto>>
        {
            #region Members
            private readonly IUnitOfWork _unitOfWork;
            private readonly IProfileReader _profileReader;
            #endregion


            #region Constructor
            public Handler(IUnitOfWork unitOfWork, IProfileReader profileReader, IMapperHelper mapperHelper)
            {
                _unitOfWork = unitOfWork;
                _profileReader = profileReader;
            }
            #endregion


            #region Methods
            public async Task<IEnumerable<ProfileDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                ICollection<ProfileDto> list = new List<ProfileDto>();
                IEnumerable<UserFollower> userFollowers = null;
                switch (request.Predicate)
                {
                    case "followers":
                        userFollowers = await _unitOfWork.UserFollowerRepo.GetFollowers(request.AppUserId);
                        if (userFollowers?.Any() == true)
                        {
                            IEnumerable<Guid> followerIdList = userFollowers.Select(f => f.FollowerId);
                            foreach (var followerId in followerIdList)
                            {
                                ProfileDto profileDto = await _profileReader.ReadProfile(followerId, cancellationToken);
                                list.Add(profileDto);
                            }
                        }
                        break;
                    case "followings":
                        userFollowers = await _unitOfWork.UserFollowerRepo.GetFollowing(request.AppUserId);
                        if (userFollowers?.Any() == true)
                        {
                            IEnumerable<Guid> followingIdList = userFollowers.Select(f => f.UserId);
                            foreach (var followingId in followingIdList)
                            {
                                ProfileDto profileDto = await _profileReader.ReadProfile(followingId, cancellationToken);
                                list.Add(profileDto);
                            }
                        }
                        break;
                }
                return list;
            }
            #endregion
        }
    }
}
