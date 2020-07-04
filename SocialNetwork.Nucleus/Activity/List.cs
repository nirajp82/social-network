using MediatR;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using SocialNetwork.DataModel;
using SocialNetwork.Dto;
using SocialNetwork.EF.Repo;
using SocialNetwork.Nucleus.Interfaces;
using SocialNetwork.Util;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SocialNetwork.Nucleus.Engine.Activity
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
            public Query(int? offSet, int? limit)
            {
                Offset = offSet ?? 0;
                Limit = limit ?? 5;
            }

            public int Offset { get; set; }
            public int Limit { get; set; }
        }

        public class Handler : IRequestHandler<Query, ActivityEnvelope>
        {
            #region Members
            private readonly IUnitOfWork _unitOfWork;
            private readonly IUserActivityHelper _userActivityHelper;
            #endregion


            #region Constuctor
            public Handler(IUnitOfWork unitOfWork, IUserActivityHelper userActivityHelper)
            {
                _unitOfWork = unitOfWork;
                _userActivityHelper = userActivityHelper;
            }
            #endregion


            #region Methods
            public async Task<ActivityEnvelope> Handle(Query request, CancellationToken cancellationToken)
            {
                var dbResponse = await _unitOfWork.ActivityRepo.GetAllAsync(request.Offset, request.Limit, cancellationToken);
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