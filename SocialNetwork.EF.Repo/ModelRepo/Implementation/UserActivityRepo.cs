using Microsoft.EntityFrameworkCore;
using SocialNetwork.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<IEnumerable<Activity>> GetUserActivities(Guid appUserId, string predicate, CancellationToken cancellationToken)
        {
            var queryable = base.Find(e => e.AppUserId == appUserId)
                                            .Include(nameof(UserActivity.Activity));

            switch (predicate)
            {
                case "past":
                    queryable = queryable.Where(a => a.Activity.Date < DateTime.Now);
                    break;
                case "hosting":
                    queryable = queryable.Where(a => a.IsHost);
                    break;
                default:
                    queryable = queryable.Where(a => a.Activity.Date >= DateTime.Now);
                    break;
            }
            return await (from UserActivity in queryable
                          select new Activity
                          {
                              Id = UserActivity.Activity.Id,
                              Category = UserActivity.Activity.Category,
                              Title = UserActivity.Activity.Title,
                              Date = UserActivity.Activity.Date,
                              Description = UserActivity.Activity.Description
                          }).ToListAsync(cancellationToken);
        }
        #endregion
    }
}
