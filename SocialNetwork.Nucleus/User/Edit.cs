using FluentValidation;
using MediatR;
using SocialNetwork.DataModel;
using SocialNetwork.EF.Repo;
using SocialNetwork.Util;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace SocialNetwork.Nucleus.Engine.User
{
    public class Edit
    {
        public class Command : IRequest<string>
        {
            public string FirstName { get; set; }

            public string LastName { get; set; }

            public string Email { get; set; }

            public string Bio { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(c => c.FirstName).NotEmpty().MaximumLength(24);
                RuleFor(c => c.LastName).NotEmpty().MaximumLength(24);
                RuleFor(c => c.Email).NotEmpty().EmailAddress().MaximumLength(24);
                RuleFor(c => c.Bio).NotEmpty().MaximumLength(240);
            }
        }

        public class Handler : IRequestHandler<Command, string>
        {
            #region Members
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapperHelper _mapperHelper;
            private readonly IUserAccessor _userAccessor;
            #endregion


            #region Constructor
            public Handler(IUnitOfWork unitOfWork, IMapperHelper mapperHelper, IUserAccessor userAccessor)
            {
                _unitOfWork = unitOfWork;
                _mapperHelper = mapperHelper;
                _userAccessor = userAccessor;
            }
            #endregion


            #region Public Methods
            public async Task<string> Handle(Command request, CancellationToken cancellationToken)
            {
                Guid appUserId = _userAccessor.GetCurrentUserId();
                AppUser dbAppUser = await _unitOfWork.AppUserRepo.FindFirstAsync(e => e.Id == appUserId, null, cancellationToken);

                await Validate(request, dbAppUser.Id);
                AppUser appUser = _mapperHelper.Map<Command, AppUser>(request);
                appUser.Id = dbAppUser.Id;
                _unitOfWork.AppUserRepo.Update(appUser);
                int insertCnt = await _unitOfWork.SaveAsync(cancellationToken);

                if (insertCnt > 0)
                    return appUser.DisplayName;

                throw new Exception("Problem saving changes to database");
            }
            #endregion


            #region Private Methods
            private async Task Validate(Command request, Guid appUserId)
            {
                bool exits = await _unitOfWork.AppUserRepo.HasAnyAsync(e => e.Email == request.Email && e.Id != appUserId);
                if (exits)
                    throw new CustomException(HttpStatusCode.BadRequest, new { Email = "Email already exists!" });
            }
            #endregion
        }
    }
}
