using SocialNetwork.DataModel;
using System;
using System.Collections.Generic;
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

        public async Task<AppUser> GetUserProfile(Guid appUserId, CancellationToken cancellationToken = default)
        {
            AppUser appUser = await FindFirstAsync(e => e.Id == appUserId,
                                        new List<string>
                                        {
                                           nameof(AppUser.IdentityUser),
                                           nameof(AppUser.Photos),
                                           //nameof(AppUser.Followers),
                                           //nameof(AppUser.Followings),
                                        }, cancellationToken);           
            return appUser;
        }
        #endregion
    }
}
