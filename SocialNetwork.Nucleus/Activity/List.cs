using MediatR;
using SocialNetwork.Dto;
using SocialNetwork.EF.Repo;
using SocialNetwork.Nucleus.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SocialNetwork.Nucleus.Activity
{
    public class List
    {
        public class ActivityEnvelope
        {
            public IEnumerable<ActivityDto> Activities { get; set; }
            public int Count { get; set; }
        }

        public class Query : IRequest<ActivityEnvelope>
        {
            public Query(int? offSet, int? limit, bool? isGoing, bool? isHost, DateTime? startDate)
            {
                Offset = offSet ?? 0;
                Limit = limit ?? 5;
                IsGoing = isGoing.GetValueOrDefault();
                IsHost = isHost.GetValueOrDefault();
                StartDate = startDate ?? DateTime.Now;
            }

            public int Offset { get; set; }
            public int Limit { get; set; }
            public bool IsGoing { get; set; }
            public bool IsHost { get; set; }
            public DateTime StartDate { get; set; }
        }

        public class Handler : IRequestHandler<Query, ActivityEnvelope>
        {
            #region Members
            private readonly IUnitOfWork _unitOfWork;
            private readonly IUserActivityHelper _userActivityHelper;
            private readonly IUserAccessor _userAccessor;
            #endregion


            #region Constuctor
            public Handler(IUnitOfWork unitOfWork, IUserActivityHelper userActivityHelper, IUserAccessor userAccessor)
            {
                _unitOfWork = unitOfWork;
                _userActivityHelper = userActivityHelper;
                _userAccessor = userAccessor;
            }
            #endregion


            #region Methods
            public async Task<ActivityEnvelope> Handle(Query request, CancellationToken cancellationToken)
            {
                var dbResponse = await _unitOfWork.ActivityRepo.GetAllAsync(request.Offset, request.Limit, request.IsGoing,
                    request.IsHost, request.StartDate, _userAccessor.GetCurrentUserId(), cancellationToken);

                ActivityEnvelope envelope = new ActivityEnvelope
                {
                    Activities = await _userActivityHelper.PrepareActivities(dbResponse.List),
                    Count = dbResponse.Count
                };
                return envelope;
            }
            #endregion
        }
    }
}