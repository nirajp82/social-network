using MediatR;
using SocialNetwork.DataModel;
using SocialNetwork.EF.Repo;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SocialNetwork.Nucleus.Engine.Activity
{
    public class AttendanceRequirement
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
                bool isUserAttending = await _unitOfWork.UserActivityRepo.ExistsAsync(request.ActivityId, _userAccessor.GetCurrentUserId(), cancellationToken);
                if (isUserAttending)
                    return new KeyValuePair<bool, object>(false, new { Activity = "User is already attending this activity!" });

                return new KeyValuePair<bool, object>(true, null);
            }
            #endregion
        }
    }
}