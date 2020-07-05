using SocialNetwork.DataModel;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SocialNetwork.EF.Repo
{
    public interface IUserActivityRepo
    {
        void Add(UserActivity entity);

        Task<IEnumerable<Activity>> GetUserActivities(Guid appUserId, string predicate, CancellationToken cancellationToken);

        Task<bool> ExistsAsync(Guid activityId, Guid appUserId, CancellationToken cancellationToken = default);

        Task<bool> IsHostAsync(Guid activityId, Guid appUserId, CancellationToken cancellationToken = default);

        Task<UserActivity> FindFirstAsync(Guid activityId, Guid appUserId, CancellationToken cancellationToken = default);

        void Delete(UserActivity entity);
    }
}
