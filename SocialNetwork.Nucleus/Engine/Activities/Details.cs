using MediatR;
using SocialNetwork.APIEntity;
using SocialNetwork.DataModel;
using SocialNetwork.EF.Repo;
using SocialNetwork.Nucleus.Helper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SocialNetwork.Nucleus.Engine.Activities
{
    public class Details
    {
        public class Query : IRequest<ActivityEntity>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, ActivityEntity>
        {
            #region Members
            private IUnitOfWork _unitOfWork { get; }
            private IMapperHelper _mapperHelper { get; }
            #endregion


            #region Constuctor
            public Handler(IUnitOfWork unitOfWork, IMapperHelper mapperHelper)
            {
                _unitOfWork = unitOfWork;
                _mapperHelper = mapperHelper;
            }
            #endregion


            #region Methods
            public async Task<ActivityEntity> Handle(Query request, CancellationToken cancellationToken)
            {
                Activity result = await _unitOfWork.ActivityRepository.FindFirstAsync(request.Id, cancellationToken);
                return _mapperHelper.Map<Activity, ActivityEntity>(result);
            }
            #endregion
        }
    }
}
