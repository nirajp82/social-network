using Microsoft.EntityFrameworkCore;
using SocialNetwork.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public async Task<IEnumerable<Activity>> FindAllAsync()
        {
            IQueryable<Activity> result = base.FindAll();
            return await result.ToListAsync();
        }

        public async Task<Activity> FindFirstAsync(Guid activityId)
        {
            return await base.FindFirstAsync(e => e.Id == activityId);
        }
        #endregion
    }
}
