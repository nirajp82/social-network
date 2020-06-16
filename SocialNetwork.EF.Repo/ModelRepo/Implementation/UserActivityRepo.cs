using SocialNetwork.DataModel;

namespace SocialNetwork.EF.Repo
{
    internal class UserActivityRepo : RepositoryBase<UserActivity>, IUserActivityRepo
    {
        #region Constructor
        public UserActivityRepo(ApplicationContext context) : base(context)
        {

        }
        #endregion


        #region Public Method
        #endregion
    }
}
