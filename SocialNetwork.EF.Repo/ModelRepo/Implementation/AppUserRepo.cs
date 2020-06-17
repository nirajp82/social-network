using SocialNetwork.DataModel;
using System.Threading;
using System.Threading.Tasks;

namespace SocialNetwork.EF.Repo
{
    internal class AppUserRepo : RepositoryBase<AppUser>, IAppUserRepo
    {
        #region Constructor
        public AppUserRepo(ApplicationContext context) : base(context)
        {
        }

        #endregion


        #region Public Method
        public async Task<AppUser> FindByUserName(string userName, CancellationToken cancellationToken = default)
        {
            return await FindFirstAsync(e => e.IdentityUser.UserName == userName, null, cancellationToken);
        }
        #endregion
    }
}
