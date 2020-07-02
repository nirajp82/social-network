using Microsoft.EntityFrameworkCore;
using SocialNetwork.DataModel;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SocialNetwork.EF.Repo
{
    internal class UserFollowerRepo : RepositoryBase<UserFollower>, IUserFollowerRepo
    {
        #region Constructor
        public UserFollowerRepo(ApplicationContext context) : base(context)
        {
        }
        #endregion


        #region Public Method
        public async Task<IEnumerable<UserFollower>> GetFollowers(Guid userId, CancellationToken cancellationToken = default)
        {
            return await base.Find(e => e.UserId == userId).ToListAsync(cancellationToken);
        }

        public async Task<long> GetFollowersCountAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            return await base.CountAsync(e => e.UserId == userId, cancellationToken);
        }

        public async Task<IEnumerable<UserFollower>> GetFollowing(Guid followerId, CancellationToken cancellationToken = default)
        {
            return await base.Find(e => e.FollowerId == followerId).ToListAsync(cancellationToken);
        }

        public async Task<long> GetFollowingCountAsync(Guid followerId, CancellationToken cancellationToken = default)
        {
            return await base.CountAsync(e => e.FollowerId == followerId, cancellationToken);
        }
        #endregion
    }
}
