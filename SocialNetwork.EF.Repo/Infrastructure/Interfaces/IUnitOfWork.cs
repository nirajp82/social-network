using System.Threading;
using System.Threading.Tasks;

namespace SocialNetwork.EF.Repo
{
    public interface IUnitOfWork
    {
        IActivityRepo ActivityRepo { get; }
        IAppUserRepo AppUserRepo { get; }
        ICommentRepo CommentRepo { get; }
        IIdentityUserRepo IdentityUserRepo { get; }
        IPhotoRepo PhotoRepo { get; }
        IUserActivityRepo UserActivityRepo { get; }
        IUserFollowerRepo UserFollowerRepo { get; }
        IValueRepo ValueRepo { get; }

        Task<int> SaveAsync(CancellationToken cancellationToken = default);
    }
}
