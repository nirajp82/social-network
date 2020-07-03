using MediatR;
using Microsoft.AspNetCore.Mvc.TagHelpers;
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
        public class Query : IRequest<IEnumerable<ActivityDto>>
        {
        }

        public class Handler : IRequestHandler<Query, IEnumerable<ActivityDto>>
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
            public async Task<IEnumerable<ActivityDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var dbResult = await _unitOfWork.ActivityRepo.GetAllAsync(cancellationToken);
                return await _userActivityHelper.PrepareActivities(dbResult);
            }
            #endregion
        }
    }
}