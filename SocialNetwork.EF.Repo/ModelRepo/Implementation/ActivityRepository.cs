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
        public async Task<IEnumerable<Activity>> GetAllAsync(CancellationToken cancellationToken)
        {
            IQueryable<Activity> result = base.GetAll();
            return await result.ToListAsync(cancellationToken);
        }

        public async Task<Activity> FindFirstAsync(Guid activityId, CancellationToken cancellationToken)
        {
            return await base.FindFirstAsync(e => e.Id == activityId, null, cancellationToken);
        }

        public async Task<bool> ExistsAsync(Guid activityId, CancellationToken cancellationToken)
        {
            return await base.HasAnyAsync(e => e.Id == activityId, cancellationToken);
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            await base.DeleteAsync(e => e.Id == id, cancellationToken);
        }
        #endregion
    }
}
