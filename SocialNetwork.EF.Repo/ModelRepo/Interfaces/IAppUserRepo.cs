using SocialNetwork.DataModel;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace SocialNetwork.EF.Repo
{
    public interface IAppUserRepo
    {
        Task<bool> HasAnyAsync(Expression<Func<AppUser, bool>> predicate,
                    CancellationToken cancellationToken = default);

        Task<AppUser> FindFirstAsync(Expression<Func<AppUser, bool>> predicate,
           IEnumerable<string> includes = null,
           CancellationToken cancellationToken = default);

        Task<AppUser> FindByUserName(string userName, CancellationToken cancellationToken = default);

        Task<AppUser> GetUserProfile(Guid appUserId, CancellationToken cancellationToken = default);

        void Add(AppUser user);
    }
}
