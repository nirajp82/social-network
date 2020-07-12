using FluentValidation;
using MediatR;
using SocialNetwork.Dto;
using SocialNetwork.DataModel;
using SocialNetwork.EF.Repo;
using SocialNetwork.Util;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace SocialNetwork.Nucleus.User
{
    public class Login
    {
        public class Command : IRequest<UserDto>
        {
            public string UserName { get; set; }
            public string Password { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                RuleFor(l => l.UserName).NotEmpty();
                RuleFor(l => l.Password).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<Command, UserDto>
        {
            #region Members
            private readonly IJwtGenerator _jwtGenerator;
            private readonly IUnitOfWork _unitOfWork;
            private readonly ICryptoHelper _cryptoHelper;
            private readonly IMapperHelper _mapperHelper;
            #endregion


            #region Constuctor
            public Handler(IUnitOfWork unitOfWork, 
                IJwtGenerator jwtGenerator, UtilFactory utilFactory, IMapperHelper mapperHelper)
            {
                _unitOfWork = unitOfWork;
                _jwtGenerator = jwtGenerator;
                _cryptoHelper = utilFactory.CryptoHelper;
                _mapperHelper = mapperHelper;
            }
            #endregion


            #region Methods
            public async Task<UserDto> Handle(Command request, CancellationToken cancellationToken)
            {
                IdentityUser identityUser = await _unitOfWork.IdentityUserRepo.FindFirstAsync(request.UserName, cancellationToken);
                if (identityUser == null)
                    throw new CustomException(HttpStatusCode.Unauthorized);

                if (_cryptoHelper.GenerateHash(request.Password, identityUser.Salt) == identityUser.Passoword)
                {
                    identityUser.RefreshToken = _jwtGenerator.CreateRefreshToken();
                    identityUser.RefreshTokenExpiry = HelperFunc.GetCurrentDateTime().AddDays(30);
                    _unitOfWork.IdentityUserRepo.Update(identityUser);

                    await _unitOfWork.SaveAsync(cancellationToken);

                    return _mapperHelper.Map<IdentityUser, UserDto>(identityUser);
                }
                throw new CustomException(HttpStatusCode.Unauthorized);
            }
            #endregion
        }
    }
}