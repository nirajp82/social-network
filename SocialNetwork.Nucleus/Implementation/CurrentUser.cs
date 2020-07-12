using SocialNetwork.EF.Repo;

namespace SocialNetwork.Nucleus
{
    internal class CurrentUser : ICurrentUser
    {
        #region Members
        private readonly IUserAccessor _userAccessor;
        #endregion


        #region Constructor
        public CurrentUser(IUserAccessor userAccessor)
        {
            _userAccessor = userAccessor;
        }
        #endregion


        #region Public Methods
        public string GetName()
        {
            return _userAccessor.GetCurrentUserName();
        }
        #endregion
    }
}
