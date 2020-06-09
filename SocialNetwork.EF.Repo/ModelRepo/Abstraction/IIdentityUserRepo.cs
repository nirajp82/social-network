using SocialNetwork.DataModel;
using System.Threading;
using System.Threading.Tasks;

namespace SocialNetwork.EF.Repo
{
    public interface IIdentityUserRepo
    {
        Task<IdentityUser> FindFirstAsync(string userName, CancellationToken cancellationToken);
    }
}
