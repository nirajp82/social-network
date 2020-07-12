using FluentValidation;
using MediatR;
using SocialNetwork.Dto;
using SocialNetwork.DataModel;
using SocialNetwork.EF.Repo;
using SocialNetwork.Util;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace SocialNetwork.Nucleus.User
{
    public class Register
    {
        public class Command : IRequest<UserDto>
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

        public class Handler : IRequestHandler<Command, UserDto>
        {
            #region Members
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapperHelper _mapperHelper;
            #endregion


            #region Constructor
            public Handler(IUnitOfWork unitOfWork, IMapperHelper mapperHelper)
            {
                _unitOfWork = unitOfWork;
                _mapperHelper = mapperHelper;
            }
            #endregion


            #region Public Methods
            public async Task<UserDto> Handle(Command command, CancellationToken cancellationToken)
            {
                await Validate(command);

                AppUser appUser = _mapperHelper.Map<Command, AppUser>(command);
                _unitOfWork.AppUserRepo.Add(appUser);

                int insertCnt = await _unitOfWork.SaveAsync(cancellationToken);

                if (insertCnt > 0)
                    return _mapperHelper.Map<AppUser, UserDto>(appUser);

                throw new Exception("Problem saving changes to database");
            }
            #endregion

            #region Private Methods
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
