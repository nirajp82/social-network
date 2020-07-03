using SocialNetwork.EF.Repo;

namespace SocialNetwork.Nucleus
{
    internal class SessionUser : ISessionUser
    {
        #region Members
        private readonly IUserAccessor _userAccessor;
        #endregion


        #region Constructor
        public SessionUser(IUserAccessor userAccessor)
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
