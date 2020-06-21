using SocialNetwork.DataModel;
using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace SocialNetwork.EF.Repo
{
    public interface IPhotoRepo
    {
        void Add(Photo entity);

        Task<int> DeleteAsync(Expression<Func<Photo, bool>> predicate, CancellationToken cancellationToken = default);
    }
}