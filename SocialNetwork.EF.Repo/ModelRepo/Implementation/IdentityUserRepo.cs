using Microsoft.EntityFrameworkCore;
using SocialNetwork.DataModel;
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
            return await base.Find(e => e.UserName == userName)
                        .Include(i => i.AppUser)
                        .ThenInclude(u => u.Photos)//.Where(p => p.IsMainPhoto))
                        .FirstOrDefaultAsync(cancellationToken);
        }
        #endregion
    }
}
