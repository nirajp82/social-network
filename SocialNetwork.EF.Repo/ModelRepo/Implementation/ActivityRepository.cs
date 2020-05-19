using Microsoft.EntityFrameworkCore;
using SocialNetwork.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SocialNetwork.EF.Repo
{
    internal class ActivityRepository : RepositoryBase<Activity>, IActivityRepository
    {
        #region Constructor
        public ActivityRepository(ApplicationContext context) : base(context)
        {
        }        
        #endregion


        #region Public Method
        public async Task<IEnumerable<Activity>> FindAllAsync(CancellationToken cancellationToken)
        {
            IQueryable<Activity> result = base.FindAll();
            return await result.ToListAsync(cancellationToken);
        }

        public async Task<Activity> FindFirstAsync(Guid activityId, CancellationToken cancellationToken)
        {
            return await base.FindFirstAsync(e => e.Id == activityId, null, cancellationToken);
        }

        public async Task<bool> ExistsAsync(Guid activityId, CancellationToken cancellationToken)
        {
            return await base.ExistsAsync(e => e.Id == activityId, cancellationToken);
        }
        #endregion
    }
}
