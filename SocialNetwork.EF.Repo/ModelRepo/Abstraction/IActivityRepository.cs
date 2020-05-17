using SocialNetwork.DataModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SocialNetwork.EF.Repo
{
    public interface IActivityRepository
    {
        Task<IEnumerable<Activity>> FindAllAsync();

        Task<Activity> FindFirstAsync(Guid activityId);
    }
}
