using SocialNetwork.DataModel;
using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace SocialNetwork.EF.Repo
{
    public interface IAppUserRepo
    {
        Task<bool> HasAnyAsync(Expression<Func<AppUser, bool>> predicate,
                    CancellationToken cancellationToken = default);

        void Add(AppUser user);
    }
}
