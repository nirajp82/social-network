using SocialNetwork.DataModel;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SocialNetwork.EF.Repo
{
    public interface IActivityRepo
    {
        void Add(Activity entity);

        Task<int> DeleteAsync(Guid id, CancellationToken cancellationToken);

        Task<ResponseEnvelope<Activity>> GetAllAsync(int offset, int limit, bool isGoing, bool isHost,
             DateTime startDate, Guid appUserId, CancellationToken cancellationToken);       

        Task<bool> ExistsAsync(Guid activityId, CancellationToken cancellationToken = default);

        Task<Activity> FindFirstAsync(Guid activityId, CancellationToken cancellationToken);

        void Update(Activity entity);
    }
}
