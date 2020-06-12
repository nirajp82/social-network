using MediatR;
using SocialNetwork.APIEntity;
using SocialNetwork.DataModel;
using SocialNetwork.EF.Repo;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SocialNetwork.Nucleus
{
    public class CurrentUser
    {
        public class Query : IRequest<UserEntity> { }

        public class Handler : IRequestHandler<Query, UserEntity>
        {
            #region Members
            private readonly IJwtGenerator _jwtGenerator;
            private readonly IUnitOfWork _unitOfWork;
            private readonly IUserAccessor _userAccessor;
            #endregion


            #region Constructor
            public Handler(IJwtGenerator jwtGenerator, IUnitOfWork unitOfWork, IUserAccessor userAccessor)
            {
                _jwtGenerator = jwtGenerator;
                _unitOfWork = unitOfWork;
                _userAccessor = userAccessor;
            }
            #endregion

            #region Public Methods
            public async Task<UserEntity> Handle(Query query, CancellationToken cancellationToken)
            {
                string userName = _userAccessor.GetCurrentUserName();
                AppUser user = await _unitOfWork.AppUserRepo.FindFirstAsync(e => e.IdentityUser.UserName == userName);
                return new UserEntity
                {
                    DisplayName = $"{user.LastName}, {user.FirstName}",
                    UserName = userName,
                    Token = _jwtGenerator.CreateToken(userName)
                };
            }
            #endregion
        }
    }
}
