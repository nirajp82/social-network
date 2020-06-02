using SocialNetwork.DataModel;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SocialNetwork.EF.Repo
{
    public interface IActivityRepository
    {
        void Add(Activity entity);

        Task<int> DeleteAsync(Guid id, CancellationToken cancellationToken);

        void Update(Activity entity);

        Task<IEnumerable<Activity>> GetAllAsync(CancellationToken cancellationToken);

        Task<Activity> FindFirstAsync(Guid activityId, CancellationToken cancellationToken);

        Task<bool> ExistsAsync(Guid activityId, CancellationToken cancellationToken = default);
    }
}
