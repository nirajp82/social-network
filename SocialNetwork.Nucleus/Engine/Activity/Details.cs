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
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapperHelper _mapperHelper;
            private readonly IPhotoAccessor _photoAccessor;
            #endregion


            #region Constuctor
            public DetailsHandler(IUnitOfWork unitOfWork, IPhotoAccessor photoAccessor, IMapperHelper mapperHelper)
            {
                _unitOfWork = unitOfWork;
                _mapperHelper = mapperHelper;
                _photoAccessor = photoAccessor;
            }
            #endregion


            #region Methods
            public async Task<ActivityDto> Handle(Query request, CancellationToken cancellationToken)
            {
                DataModel.Activity dbResult = await _unitOfWork.ActivityRepo.FindFirstAsync(request.ActivityId, cancellationToken);
                var response = _mapperHelper.Map<DataModel.Activity, ActivityDto>(dbResult);
                foreach (var item in response.Comments)
                {
                    item.UserImage = _photoAccessor.PreparePhotoUrl(item.UserImage);
                }
                return response;
            }
            #endregion
        }
    }
}
