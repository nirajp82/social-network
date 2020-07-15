using FluentValidation;
using MediatR;
using SocialNetwork.DataModel;
using SocialNetwork.Dto;
using SocialNetwork.EF.Repo;
using SocialNetwork.Util;
using System.Net;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace SocialNetwork.Nucleus.User
{
    public class RefreshToken
    {
        public class Query : IRequest<UserDto>
        {
            [JsonIgnore]
            public string UserName { get; set; }
            public string Token { get; set; }
            public string RefreshToken { get; set; }
        }

        public class CommandValidator : AbstractValidator<Query>
        {
            public CommandValidator()
            {
                RuleFor(l => l.Token).NotEmpty();
                RuleFor(l => l.RefreshToken).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<Query, UserDto>
        {
            #region Members
            private readonly IJwtGenerator _jwtGenerator;
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapperHelper _mapperHelper;
            #endregion


            #region Constructor
            public Handler(IMapperHelper mapperHelper, IUnitOfWork unitOfWork, IJwtGenerator jwtGenerator)
            {
                _mapperHelper = mapperHelper;
                _unitOfWork = unitOfWork;
                _jwtGenerator = jwtGenerator;
            }
            #endregion


            #region Public Methods
            public async Task<UserDto> Handle(Query request, CancellationToken cancellationToken)
            {
                IdentityUser identityUser = await _unitOfWork.IdentityUserRepo.FindFirstAsync(request.UserName, cancellationToken);
                if (identityUser == null)
                    throw new CustomException(HttpStatusCode.BadRequest);

                //If recently expired token is matching with toke n in request please return recently generated token
                //This will be the case when two concurrent request comes from client and one request updates refresh token
                else if (request.RefreshToken == identityUser.PreviousRefreshToken && identityUser.PreviousRefreshTokenExpiry > HelperFunc.GetCurrentDateTime())
                    return _mapperHelper.Map<IdentityUser, UserDto>(identityUser);

                //Check if current refresh token is matching and valid
                else if (request.RefreshToken == identityUser.RefreshToken && identityUser.RefreshTokenExpiry > HelperFunc.GetCurrentDateTime())
                {
                    //If current valid token is expired, generate new token and return it.
                    //else return existing token that is still valid.
                    if (request.RefreshToken == identityUser.RefreshToken)
                    {
                        identityUser.PreviousRefreshToken = identityUser.RefreshToken;
                        identityUser.PreviousRefreshTokenExpiry = HelperFunc.GetCurrentDateTime().AddMinutes(2);

                        identityUser.RefreshToken = _jwtGenerator.CreateRefreshToken();
                        identityUser.RefreshTokenExpiry = HelperFunc.GetCurrentDateTime().AddDays(30);
                        _unitOfWork.IdentityUserRepo.Update(identityUser);
                        await _unitOfWork.SaveAsync(cancellationToken);
                    }
                    return _mapperHelper.Map<IdentityUser, UserDto>(identityUser);
                }
                throw new CustomException(HttpStatusCode.BadRequest);
            }
            #endregion
        }
    }
}
