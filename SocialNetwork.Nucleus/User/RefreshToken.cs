using MediatR;
using SocialNetwork.DataModel;
using SocialNetwork.Dto;
using SocialNetwork.EF.Repo;
using SocialNetwork.Util;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace SocialNetwork.Nucleus.User
{
    public class RefreshToken
    {
        public class Command : IRequest<UserDto>
        {
            public string UserName { get; set; }
            public string Token { get; set; }
            public string RefreshToken { get; set; }
        }

        public class Handler : IRequestHandler<Command, UserDto>
        {
            #region Members
            private readonly IJwtGenerator _jwtGenerator;
            private readonly IUnitOfWork _unitOfWork;
            private readonly IPhotoAccessor _photoAccessor;
            private readonly IMapperHelper _mapperHelper;
            #endregion


            #region Constructor
            public Handler(IMapperHelper mapperHelper, IUnitOfWork unitOfWork, IPhotoAccessor photoAccessor)
            {
                _mapperHelper = mapperHelper;
                _unitOfWork = unitOfWork;   
                _photoAccessor = photoAccessor;
            }
            #endregion


            #region Public Methods
            public async Task<UserDto> Handle(Command request, CancellationToken cancellationToken)
            {
                IdentityUser identityUser = await _unitOfWork.IdentityUserRepo.FindFirstAsync(request.UserName, cancellationToken);
                if (identityUser == null || identityUser.RefreshToken != request.RefreshToken || identityUser.RefreshTokenExpiry > HelperFunc.GetCurrentDateTime())
                    throw new CustomException(HttpStatusCode.BadRequest);

                identityUser.RefreshToken = _jwtGenerator.CreateRefreshToken();
                identityUser.RefreshTokenExpiry = HelperFunc.GetCurrentDateTime().AddDays(30);
                _unitOfWork.IdentityUserRepo.Update(identityUser);
                await _unitOfWork.SaveAsync(cancellationToken);

                return _mapperHelper.Map<IdentityUser, UserDto>(identityUser);
            }
            #endregion
        }
    }
}
