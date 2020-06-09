using System.Threading;
using System.Threading.Tasks;

namespace SocialNetwork.EF.Repo
{
    public interface IUnitOfWork
    {
        IActivityRepo ActivityRepo { get; }
        IValueRepo ValueRepo { get; }
        IIdentityUserRepo IdentityUserRepo { get; }
        Task<int> SaveAsync(CancellationToken cancellationToken = default);
    }
}
