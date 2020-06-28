using FluentValidation;
using MediatR;
using SocialNetwork.Dto;
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
            private readonly IPhotoAccessor _photoAccessor;
            #endregion


            #region Constuctor
            public Handler(IUnitOfWork unitOfWork, IPhotoAccessor photoAccessor,
                IJwtGenerator jwtGenerator, UtilFactory utilFactory)
            {
                _photoAccessor = photoAccessor;
                _unitOfWork = unitOfWork;
                _jwtGenerator = jwtGenerator;
                _cryptoHelper = utilFactory.CryptoHelper;
            }
            #endregion


            #region Methods
            public async Task<UserDto> Handle(Command request, CancellationToken cancellationToken)
            {
                IdentityUser user = await _unitOfWork.IdentityUserRepo.FindFirstAsync(request.UserName, cancellationToken);
                if (user == null)
                    throw new CustomException(HttpStatusCode.Unauthorized);

                if (_cryptoHelper.GenerateHash(request.Password, user.Salt) == user.Passoword)
                {
                    return new UserDto
                    {
                        AppUserId = user.AppUserId,
                        UserName = request.UserName,
                        DisplayName = user.AppUser.DisplayName,
                        Token = _jwtGenerator.CreateToken(request.UserName),
                        Image = _photoAccessor.PreparePhotoUrl(user.AppUser.MainPhoto?.CloudFileName)
                    };
                }
                throw new CustomException(HttpStatusCode.Unauthorized);
            }
            #endregion
        }
    }
}