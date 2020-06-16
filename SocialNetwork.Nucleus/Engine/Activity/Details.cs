using MediatR;
using SocialNetwork.DTO;
using SocialNetwork.EF.Repo;
using SocialNetwork.Util;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SocialNetwork.Nucleus.Engine.Activity
{
    public class Details
    {
        public class Query : IRequest<ActivityDTO>
        {
            public Guid Id { get; set; }
        }

        public class DetailsHandler : IRequestHandler<Query, ActivityDTO>
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
            public async Task<ActivityDTO> Handle(Query request, CancellationToken cancellationToken)
            {
                DataModel.Activity result = await _unitOfWork.ActivityRepo.FindFirstAsync(request.Id, cancellationToken);
                return _mapperHelper.Map<DataModel.Activity, ActivityDTO>(result);
            }
            #endregion
        }
    }
}
