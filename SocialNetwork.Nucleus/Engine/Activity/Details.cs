using MediatR;
using SocialNetwork.Dto;
using SocialNetwork.EF.Repo;
using SocialNetwork.Util;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SocialNetwork.Nucleus.Engine.Activity
{
    public class Details
    {
        public class Query : IRequest<ActivityDto>
        {
            public Guid ActivityId { get; set; }
        }

        public class DetailsHandler : IRequestHandler<Query, ActivityDto>
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
            public async Task<ActivityDto> Handle(Query request, CancellationToken cancellationToken)
            {
                DataModel.Activity result = await _unitOfWork.ActivityRepo.FindFirstAsync(request.ActivityId, cancellationToken);
                return _mapperHelper.Map<DataModel.Activity, ActivityDto>(result);
            }
            #endregion
        }
    }
}
