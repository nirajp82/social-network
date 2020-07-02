using MediatR;
using SocialNetwork.Dto;
using SocialNetwork.EF.Repo;
using SocialNetwork.Util;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SocialNetwork.Nucleus.Engine.Activity
{
    public class List
    {
        public class Query : IRequest<IEnumerable<ActivityDto>>
        {
        }

        public class Handler : IRequestHandler<Query, IEnumerable<ActivityDto>>
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
            public async Task<IEnumerable<ActivityDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var dbResult = await _unitOfWork.ActivityRepo.GetAllAsync(cancellationToken);
                IEnumerable<ActivityDto> reponse = _mapperHelper.MapList<DataModel.Activity, ActivityDto>(dbResult);
                return reponse;
            }
            #endregion
        }
    }
}