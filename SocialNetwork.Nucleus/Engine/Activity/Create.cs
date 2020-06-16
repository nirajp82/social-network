using FluentValidation;
using MediatR;
using SocialNetwork.DataModel;
using SocialNetwork.EF.Repo;
using SocialNetwork.Util;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SocialNetwork.Nucleus.Engine.Activity
{
    public class Create
    {
        public class Command : IRequest<Guid>
        {
            public string Title { get; set; }

            public string Description { get; set; }

            public string Category { get; set; }

            public DateTime Date { get; set; }

            public string City { get; set; }

            public string Venue { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(c => c.Title).NotEmpty();
                RuleFor(c => c.Description).NotEmpty();
                RuleFor(c => c.Category).NotEmpty();
                RuleFor(c => c.Date).NotEmpty();
                RuleFor(c => c.City).NotEmpty();
                RuleFor(c => c.Venue).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<Command, Guid>
        {
            #region Members
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapperHelper _mapperHelper;
            private readonly IUserAccessor _userAccessor;
            #endregion


            #region Constuctor
            public Handler(IUnitOfWork unitOfWork, IMapperHelper mapperHelper, IUserAccessor userAccessor)
            {
                _unitOfWork = unitOfWork;
                _mapperHelper = mapperHelper;
                _userAccessor = userAccessor;
            }
            #endregion


            #region Methods
            public async Task<Guid> Handle(Command request, CancellationToken cancellationToken)
            {
                DataModel.Activity activity = _mapperHelper.Map<Command, DataModel.Activity>(request);
                //Generate new Id for new Entity
                activity.Id = Guid.NewGuid();
                _unitOfWork.ActivityRepo.Add(activity);

                AppUser appUser = await _unitOfWork.AppUserRepo.FindFirstAsync(e =>
                                        e.IdentityUser.UserName == _userAccessor.GetCurrentUserName());

                UserActivity userActivity = new UserActivity
                {
                    Activity = activity,
                    IsHost = true,
                    DateJoined = HelperFunc.GetCurrentDateTime(),
                    AppUser = appUser
                };
                _unitOfWork.UserActivityRepo.Add(userActivity);

                int insertCnt = await _unitOfWork.SaveAsync(cancellationToken);
                if (insertCnt > 0)
                    return activity.Id;

                throw new Exception("Problem saving changes to database");
            }
            #endregion
        }
    }
}
