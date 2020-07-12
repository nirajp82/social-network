using MediatR;
using SocialNetwork.Dto;
using SocialNetwork.EF.Repo;
using SocialNetwork.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SocialNetwork.Nucleus.User
{
    public class UserActivity
    {
        public class Query : IRequest<IEnumerable<UserActivityDto>>
        {
            public Guid AppUserId { get; set; }
            public string Predicate { get; set; }
        }

        public class Handler : IRequestHandler<Query, IEnumerable<UserActivityDto>>
        {
            #region Members
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapperHelper _mapperHelper;
            #endregion


            #region Constructor
            public Handler(IMapperHelper mapperHelper, IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
                _mapperHelper = mapperHelper;
            }
            #endregion


            #region Public Methods
            public async Task<IEnumerable<UserActivityDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                IEnumerable<DataModel.Activity> dbResult = await _unitOfWork.UserActivityRepo.GetUserActivities(request.AppUserId, request.Predicate, cancellationToken);
                var result = _mapperHelper.MapList<DataModel.Activity, UserActivityDto>(dbResult);
                return result?.OrderBy(a => a.Date);
            }
            #endregion
        }
    }
}
