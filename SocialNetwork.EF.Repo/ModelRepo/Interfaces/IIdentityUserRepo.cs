using SocialNetwork.DataModel;
using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace SocialNetwork.EF.Repo
{
    public interface IIdentityUserRepo
    {
        Task<bool> HasAnyAsync(Expression<Func<IdentityUser, bool>> predicate,
                    CancellationToken cancellationToken = default);

        Task<IdentityUser> FindFirstAsync(string userName, CancellationToken cancellationToken);

        void Update(IdentityUser entity);
    }
}
