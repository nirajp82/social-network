using FluentValidation;
using MediatR;
using SocialNetwork.DataModel;
using SocialNetwork.EF.Repo;
using SocialNetwork.Infrastructure;
using SocialNetwork.Util;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace SocialNetwork.Nucleus.Engine.User
{
    public class Login
    {
        public class User
        {
            public string DisplayName { get; set; }
            public string UserName { get; set; }
            public string Token { get; set; }
            public string Image { get; set; }
        }

        public class LoginQuery : IRequest<User>
        {
            public string UserName { get; set; }
            public string Password { get; set; }
        }

        public class LoginValidator : AbstractValidator<LoginQuery>
        {
            public LoginValidator()
            {
                RuleFor(l => l.UserName).NotEmpty();
                RuleFor(l => l.Password).NotEmpty();
            }
        }

        public class LoginHandler : IRequestHandler<LoginQuery, User>
        {
            #region Members
            private IJwtGenerator _jwtGenerator { get; }
            private IUnitOfWork _unitOfWork { get; }
            private ICryptoHelper _cryptoHelper { get; }
            #endregion


            #region Constuctor
            public LoginHandler(IUnitOfWork unitOfWork,IJwtGenerator jwtGenerator,   UtilFactory utilFactory)
            {
                _unitOfWork = unitOfWork;
                _jwtGenerator = jwtGenerator;
                _cryptoHelper = utilFactory.CryptoHelper;
            }
            #endregion


            #region Methods
            public async Task<User> Handle(LoginQuery request, CancellationToken cancellationToken)
            {
                IdentityUser user = await _unitOfWork.IdentityUserRepo.FindFirstAsync(request.UserName, cancellationToken);
                if (user == null)
                    throw new CustomException(HttpStatusCode.Unauthorized);

                if (_cryptoHelper.GenerateHash(request.Password, user.Salt) == user.Passoword)
                {
                    //TODO: Generate Token
                    return new User
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
