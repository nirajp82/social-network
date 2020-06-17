using MediatR;
using SocialNetwork.DataModel;
using SocialNetwork.EF.Repo;
using SocialNetwork.Util;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace SocialNetwork.Nucleus.Engine.Activity
{
    public class Attend
    {
        public class Command : IRequest
        {
            public Guid ActivityId { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            #region Members
            private readonly IUnitOfWork _unitOfWork;
            private readonly IUserAccessor _userAccessor;
            #endregion


            #region Constuctor
            public Handler(IUnitOfWork unitOfWork, IUserAccessor userAccessor)
            {
                _unitOfWork = unitOfWork;
                _userAccessor = userAccessor;
            }
            #endregion


            #region Methods
            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                AppUser appUser = await Validate(request, cancellationToken);

                UserActivity hostAttendee = new UserActivity
                {
                    ActivityId = request.ActivityId,
                    IsHost = false,
                    DateJoined = HelperFunc.GetCurrentDateTime(),
                    AppUserId = appUser.Id
                };
                _unitOfWork.UserActivityRepo.Add(hostAttendee);

                int insertCnt = await _unitOfWork.SaveAsync(cancellationToken);
                if (insertCnt > 0)
                    return Unit.Value;

                throw new Exception("Problem saving changes to database");
            }
            #endregion

            #region Private Method
            private async Task<AppUser> Validate(Command request, CancellationToken cancellationToken)
            {
                bool activityExists = await _unitOfWork.ActivityRepo.ExistsAsync(request.ActivityId, cancellationToken);
                if (!activityExists)
                    throw new CustomException(HttpStatusCode.NotFound, new { Activity = "Could not found activity!" });

                AppUser appUser = await _unitOfWork.AppUserRepo.FindByUserName(_userAccessor.GetCurrentUserName());
                bool isUserAttending = await _unitOfWork.UserActivityRepo.ExistsAsync(request.ActivityId, appUser.Id, cancellationToken);
                if (isUserAttending)
                    throw new CustomException(HttpStatusCode.BadRequest, new { Activity = "User is already attending this activity!" });

                return appUser;
            }
            #endregion
        }
    }
}