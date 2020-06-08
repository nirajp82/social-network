using MediatR;
using SocialNetwork.APIEntity;
using SocialNetwork.DataModel;
using SocialNetwork.EF.Repo;
using SocialNetwork.Nucleus.Helper;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SocialNetwork.Nucleus.Engine.Activities
{
    public class Details
    {
        public class DetailsQuery : IRequest<ActivityEntity>
        {
            public Guid Id { get; set; }
        }

        public class DetailsHandler : IRequestHandler<DetailsQuery, ActivityEntity>
        {
            #region Members
            private IUnitOfWork _unitOfWork { get; }
            private IMapperHelper _mapperHelper { get; }
            #endregion


            #region Constuctor
            public DetailsHandler(IUnitOfWork unitOfWork, IMapperHelper mapperHelper)
            {
                _unitOfWork = unitOfWork;
                _mapperHelper = mapperHelper;
            }
            #endregion


            #region Methods
            public async Task<ActivityEntity> Handle(DetailsQuery request, CancellationToken cancellationToken)
            {
                Activity result = await _unitOfWork.ActivityRepository.FindFirstAsync(request.Id, cancellationToken);
                return _mapperHelper.Map<Activity, ActivityEntity>(result);
            }
            #endregion
        }
    }
}
