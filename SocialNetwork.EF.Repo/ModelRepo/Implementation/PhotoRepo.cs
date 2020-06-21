using SocialNetwork.DataModel;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SocialNetwork.EF.Repo
{
    internal class PhotoRepo : RepositoryBase<Photo>, IPhotoRepo
    {
        #region Constructor
        public PhotoRepo(ApplicationContext context) : base(context)
        {
        }
        #endregion
    }
}
