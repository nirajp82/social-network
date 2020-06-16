﻿using MediatR;
using SocialNetwork.DTO;
using SocialNetwork.EF.Repo;
using SocialNetwork.Nucleus.Helper;
using SocialNetwork.Util;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SocialNetwork.Nucleus.Engine.Activity
{
    public class List
    {       
        public class Query : IRequest<IEnumerable<ActivityDTO>>
        {
        }

        public class Handler : IRequestHandler<Query, IEnumerable<ActivityDTO>>
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
            public async Task<IEnumerable<ActivityDTO>> Handle(Query request, CancellationToken cancellationToken)
            {
                var result = await _unitOfWork.ActivityRepo.GetAllAsync(cancellationToken);
                return _mapperHelper.MapList<DataModel.Activity, ActivityDTO>(result);
            }
            #endregion
        }
    }
}