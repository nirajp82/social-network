using MediatR;
using SocialNetwork.Dto;
using SocialNetwork.DataModel;
using SocialNetwork.EF.Repo;
using System.Threading;
using System.Threading.Tasks;
using SocialNetwork.Nucleus.Engine.Activity;
using System.Collections.Generic;
using System.Linq;
using SocialNetwork.Nucleus.Engine;

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
            private readonly IPhotoAccessor _photoAccessor;
            #endregion


            #region Constructor
            public Handler(IPhotoAccessor photoAccessor, IJwtGenerator jwtGenerator,
                IUnitOfWork unitOfWork, IUserAccessor userAccessor)
            {
                _photoAccessor = photoAccessor;
                _jwtGenerator = jwtGenerator;
                _unitOfWork = unitOfWork;
                _userAccessor = userAccessor;
            }
            #endregion


            #region Public Methods
            public async Task<UserDto> Handle(Query query, CancellationToken cancellationToken)
            {
                string userName = _userAccessor.GetCurrentUserName();
                AppUser user = await _unitOfWork.AppUserRepo.FindFirstAsync(e => e.IdentityUser.UserName == userName,
                                    new List<string> { nameof(AppUser.Photos) },
                                    cancellationToken);
                return new UserDto
                {
                    AppUserId = user.Id,
                    UserName = userName,
                    DisplayName = user.DisplayName,
                    Token = _jwtGenerator.CreateToken(userName),
                    Image = _photoAccessor.PreparePhotoUrl(user.MainPhoto?.CloudFileName)
                };
            }
            #endregion
        }
    }
}
