using System.Threading;
using System.Threading.Tasks;

namespace SocialNetwork.EF.Repo
{
    public interface IUnitOfWork
    {
        IActivityRepository ActivityRepository { get; }
        IValueRepository ValueRepository { get; }
        Task<int> SaveAsync(CancellationToken cancellationToken = default);
    }
}
