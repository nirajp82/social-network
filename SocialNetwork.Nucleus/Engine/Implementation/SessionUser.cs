using Microsoft.AspNetCore.Http;
using SocialNetwork.Dto;
using SocialNetwork.EF.Repo;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SocialNetwork.Nucleus
{
    class SessionUser : ISessionUser
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
