using FluentValidation;
using MediatR;
using SocialNetwork.APIEntity;
using SocialNetwork.DataModel;
using SocialNetwork.EF.Repo;
using SocialNetwork.Util;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace SocialNetwork.Nucleus.Engine.User
{
    public class Login
    {
        public class Command : IRequest<UserEntity>
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

        public class Handler : IRequestHandler<Command, UserEntity>
        {
            #region Members
            private IJwtGenerator _jwtGenerator { get; }
            private IUnitOfWork _unitOfWork { get; }
            private ICryptoHelper _cryptoHelper { get; }
            #endregion


            #region Constuctor
            public Handler(IUnitOfWork unitOfWork, IJwtGenerator jwtGenerator, UtilFactory utilFactory)
            {
                _unitOfWork = unitOfWork;
                _jwtGenerator = jwtGenerator;
                _cryptoHelper = utilFactory.CryptoHelper;
            }
            #endregion


            #region Methods
            public async Task<UserEntity> Handle(Command request, CancellationToken cancellationToken)
            {
                IdentityUser user = await _unitOfWork.IdentityUserRepo.FindFirstAsync(request.UserName, cancellationToken);
                if (user == null)
                    throw new CustomException(HttpStatusCode.Unauthorized);

                if (_cryptoHelper.GenerateHash(request.Password, user.Salt) == user.Passoword)
                {
                    return new UserEntity
                    {
                        UserName = request.UserName,
                        DisplayName = $"{user.AppUser.LastName}, {user.AppUser.FirstName}",
                        Token = _jwtGenerator.CreateToken(request.UserName)
                    };
                }
                throw new CustomException(HttpStatusCode.Unauthorized);
            }
            #endregion
        }
    }
}