using FluentValidation;
using MediatR;
using SocialNetwork.DTO;
using SocialNetwork.DataModel;
using SocialNetwork.EF.Repo;
using SocialNetwork.Util;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace SocialNetwork.Nucleus.Engine.User
{
    public class Register
    {
        public class Command : IRequest<UserDTO>
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }
            public string UserName { get; set; }
            public string Password { get; set; }
        }

        public class CommandValdiator : AbstractValidator<Command>
        {
            public CommandValdiator()
            {
                RuleFor(c => c.FirstName).NotEmpty().MinimumLength(2).MaximumLength(24);
                RuleFor(c => c.LastName).NotEmpty().MinimumLength(2).MaximumLength(24);
                RuleFor(c => c.Email).NotEmpty().EmailAddress();
                RuleFor(c => c.UserName).NotEmpty().MinimumLength(6).MaximumLength(24);
                RuleFor(c => c.Password).Password();
            }
        }

        public class Handler : IRequestHandler<Command, UserDTO>
        {
            #region Members
            private readonly ICryptoHelper _cryptoHelper;
            private readonly IJwtGenerator _jwtGenerator;
            private readonly IUnitOfWork _unitOfWork;
            #endregion


            #region Constructor
            public Handler(IJwtGenerator jwtGenerator, IUnitOfWork unitOfWork, UtilFactory utilFactory)
            {
                _jwtGenerator = jwtGenerator;
                _cryptoHelper = utilFactory.CryptoHelper;
                _unitOfWork = unitOfWork;
            }
            #endregion

            #region Public Methods
            public async Task<UserDTO> Handle(Command user, CancellationToken cancellationToken)
            {
                await Validate(user);

                AppUser appUser = CreateAppUser(user);
                _unitOfWork.AppUserRepo.Add(appUser);

                int insertCnt = await _unitOfWork.SaveAsync();

                if (insertCnt > 0)
                {
                    return new UserDTO
                    {
                        DisplayName = $"{user.LastName}, {user.FirstName}",
                        UserName = user.UserName,
                        Token = _jwtGenerator.CreateToken(user.UserName)
                    };
                }

                throw new Exception("Problem saving changes to database");
            }           
            #endregion

            #region Private Methods
            private AppUser CreateAppUser(Command request)
            {
                string salt = _cryptoHelper.CreateBase64Salt();
                AppUser appUser = new AppUser
                {
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Email = request.Email,
                    IdentityUser = new IdentityUser
                    {
                        UserName = request.UserName,
                        Salt = salt,
                        Passoword = _cryptoHelper.GenerateHash(request.Password, salt)
                    }
                };
                return appUser;
            }

            private async Task Validate(Command user)
            {
                bool exits = await _unitOfWork.AppUserRepo.HasAnyAsync(e => e.Email == user.Email);
                if (exits)
                    throw new CustomException(HttpStatusCode.BadRequest, new { Email = "Email already exists!" });

                exits = await _unitOfWork.IdentityUserRepo.HasAnyAsync(e => e.UserName == user.UserName);
                if (exits)
                    throw new CustomException(HttpStatusCode.BadRequest, new { Username = "Username already exists!" });
            }
            #endregion
        }
    }
}
