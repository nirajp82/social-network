using SocialNetwork.DataModel;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace SocialNetwork.EF.Repo
{
    public interface IUserFollowerRepo
    {
        void Add(UserFollower entity);

        void Delete(UserFollower entity);

        Task<IEnumerable<UserFollower>> FindAsync(Expression<Func<UserFollower, bool>> predicate, CancellationToken cancellationToken = default);

        Task<long> GetFollowersCountAsync(Guid userId, CancellationToken cancellationToken = default);

        Task<long> GetFollowingCountAsync(Guid followerId, CancellationToken cancellationToken = default);

        Task<IEnumerable<UserFollower>> GetFollowers(Guid userId, CancellationToken cancellationToken = default);

        Task<IEnumerable<UserFollower>> GetFollowing(Guid followerId, CancellationToken cancellationToken = default);

        Task<bool> HasAnyAsync(Expression<Func<UserFollower, bool>> predicate, CancellationToken cancellationToken = default);
    }
}
