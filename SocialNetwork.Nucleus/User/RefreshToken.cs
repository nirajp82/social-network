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
                if (identityUser == null || identityUser.RefreshToken != request.RefreshToken 
                            || identityUser.RefreshTokenExpiry < HelperFunc.GetCurrentDateTime())
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
