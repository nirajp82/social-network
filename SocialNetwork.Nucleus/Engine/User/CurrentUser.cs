using MediatR;
using SocialNetwork.Dto;
using SocialNetwork.DataModel;
using SocialNetwork.EF.Repo;
using System.Threading;
using System.Threading.Tasks;

namespace SocialNetwork.Nucleus
{
    public class CurrentUser
    {
        public class Query : IRequest<UserDto> { }

        public class Handler : IRequestHandler<Query, UserDto>
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
            public async Task<UserDto> Handle(Query query, CancellationToken cancellationToken)
            {
                string userName = _userAccessor.GetCurrentUserName();
                AppUser user = await _unitOfWork.AppUserRepo.FindFirstAsync(e => e.IdentityUser.UserName == userName);
                return new UserDto
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
