using SocialNetwork.DataModel;
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
        #endregion
    }
}
