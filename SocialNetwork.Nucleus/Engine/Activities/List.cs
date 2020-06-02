using MediatR;
using SocialNetwork.APIEntity;
using SocialNetwork.DataModel;
using SocialNetwork.EF.Repo;
using SocialNetwork.Nucleus.Helper;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SocialNetwork.Nucleus.Engine.Activities
{
    public class List
    {       
        public class Query : IRequest<IEnumerable<ActivityEntity>>
        {
        }

        public class Handler : IRequestHandler<Query, IEnumerable<ActivityEntity>>
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
            public async Task<IEnumerable<ActivityEntity>> Handle(Query request, CancellationToken cancellationToken)
            {
                var result = await _unitOfWork.ActivityRepository.GetAllAsync(cancellationToken);
                return _mapperHelper.MapList<Activity, ActivityEntity>(result);
            }
            #endregion
        }
    }
}