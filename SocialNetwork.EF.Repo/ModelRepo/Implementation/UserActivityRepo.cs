using SocialNetwork.DataModel;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SocialNetwork.EF.Repo
{
    internal class UserActivityRepo : RepositoryBase<UserActivity>, IUserActivityRepo
    {
        #region Constructor
        public UserActivityRepo(ApplicationContext context) : base(context)
        {
        }
        #endregion


        #region Public Method
        public async Task<bool> ExistsAsync(Guid activityId, Guid appUserId, CancellationToken cancellationToken = default)
        {
            return await base.HasAnyAsync(ua => ua.AppUserId == appUserId && ua.ActivityId == activityId, cancellationToken);
        }

        public async Task<UserActivity> FindFirstAsync(Guid activityId, Guid appUserId, CancellationToken cancellationToken = default)
        {
            return await base.FindFirstAsync(ua => ua.AppUserId == appUserId && ua.ActivityId == activityId, null, cancellationToken);
        }

        public async Task<bool> IsHostAsync(Guid activityId, Guid appUserId, CancellationToken cancellationToken = default)
        {
            return await base.HasAnyAsync(ua => ua.AppUserId == appUserId && ua.ActivityId == activityId && ua.IsHost, cancellationToken);
        }
        #endregion
    }
}
