using MediatR;
using SocialNetwork.DataModel;
using SocialNetwork.EF.Repo;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SocialNetwork.Nucleus.Engine.Activity
{
    public class UnAttendanceRequirement
    {
        public class Command : IRequest<KeyValuePair<bool, object>>
        {
            public Guid ActivityId { get; set; }
        }

        public class Handler : IRequestHandler<Command, KeyValuePair<bool, object>>
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
            public async Task<KeyValuePair<bool, object>> Handle(Command request, CancellationToken cancellationToken)
            {
                AppUser appUser = await _unitOfWork.AppUserRepo.FindByUserName(_userAccessor.GetCurrentUserName());
                UserActivity userActivity = await _unitOfWork.UserActivityRepo.FindFirstAsync(request.ActivityId, appUser.Id, cancellationToken);

                if (userActivity?.IsHost == true)
                    return new KeyValuePair<bool, object>(false, new { Attendance = "Host can not be removed from activity" });

                return new KeyValuePair<bool, object>(true, null);
            }
            #endregion
        }
    }
}
