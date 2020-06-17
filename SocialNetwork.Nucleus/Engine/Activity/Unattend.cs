using MediatR;
using SocialNetwork.DataModel;
using SocialNetwork.EF.Repo;
using SocialNetwork.Util;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SocialNetwork.Nucleus.Engine.Activity
{
    public class Unattend
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
                var userActivity = await Validate(request, cancellationToken);

                if (userActivity != null)
                {
                    _unitOfWork.UserActivityRepo.Delete(userActivity);
                    int insertCnt = await _unitOfWork.SaveAsync(cancellationToken);

                    if (insertCnt <= 0)
                        throw new Exception("Problem saving changes to database");
                }
                return Unit.Value;
            }
            #endregion

            #region Private Method
            private async Task<UserActivity> Validate(Command request, CancellationToken cancellationToken)
            {
                bool activityExists = await _unitOfWork.ActivityRepo.ExistsAsync(request.ActivityId, cancellationToken);
                if (!activityExists)
                    throw new CustomException(HttpStatusCode.NotFound, new { Activity = "Could not found activity!" });

                AppUser appUser = await _unitOfWork.AppUserRepo.FindByUserName(_userAccessor.GetCurrentUserName());
                UserActivity userActivity = await _unitOfWork.UserActivityRepo.FindFirstAsync(request.ActivityId, appUser.Id, cancellationToken);

                if (userActivity?.IsHost == true)
                    throw new CustomException(HttpStatusCode.BadRequest, new { Attendance = "Host can not be removed from activity" });

                return userActivity;
            }
            #endregion
        }
    }
}
