using SocialNetwork.DataModel;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SocialNetwork.EF.Repo
{
    internal class IdentityUserRepo : RepositoryBase<IdentityUser>, IIdentityUserRepo
    {
        #region Constructor
        public IdentityUserRepo(ApplicationContext context) : base(context)
        {
        }
        #endregion


        #region Public Method
        public async Task<IdentityUser> FindFirstAsync(string userName, CancellationToken cancellationToken)
        {
            return await FindFirstAsync(e => e.UserName == userName,
                            new List<string> { nameof(IdentityUser.AppUser) },
                            cancellationToken);
        }
        #endregion
    }
}
