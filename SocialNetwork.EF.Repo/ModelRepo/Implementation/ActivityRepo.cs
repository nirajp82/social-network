using Microsoft.EntityFrameworkCore;
using SocialNetwork.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SocialNetwork.EF.Repo
{
    internal class ActivityRepo : RepositoryBase<Activity>, IActivityRepo
    {
        #region Constructor
        public ActivityRepo(ApplicationContext context) : base(context)
        {
        }
        #endregion


        #region Public Method
        public async Task<ResponseEnvelope<Activity>> GetAllAsync(int offset, int limit, bool isGoing, bool isHost,
             DateTime startDate, Guid appUserId, CancellationToken cancellationToken)
        {
            IQueryable<Activity> queryable = base.Find(null)
                                               .Include(a => a.UserActivities)
                                               .ThenInclude(ua => ua.AppUser)
                                               .ThenInclude(ua => ua.Photos);

            queryable = queryable.Where(q => q.Date >= startDate);
            if (isHost)
                queryable = queryable.Where(q => q.UserActivities.Any(a => a.IsHost && a.AppUserId == appUserId));
            else if (isGoing)
                queryable = queryable.Where(q => q.UserActivities.Any(a => a.AppUserId == appUserId));

            ResponseEnvelope<Activity> response = new ResponseEnvelope<Activity>
            {
                List = await queryable
                            .Skip(offset)
                            .Take(limit)
                            .ToListAsync(cancellationToken),
                Count = await queryable.CountAsync(cancellationToken)
            };
            return response;
        }

        public async Task<Activity> FindFirstAsync(Guid activityId, CancellationToken cancellationToken)
        {
            return await base.Find(e => e.Id == activityId)
                            .Include(a => a.UserActivities)
                            .ThenInclude(ua => ua.AppUser)
                            .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<bool> ExistsAsync(Guid activityId, CancellationToken cancellationToken)
        {
            return await base.HasAnyAsync(e => e.Id == activityId, cancellationToken);
        }

        public async Task<int> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            return await base.DeleteAsync(e => e.Id == id, cancellationToken);
        }     
        #endregion
    }
}
