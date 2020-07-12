using MediatR;
using SocialNetwork.Dto;
using SocialNetwork.DataModel;
using SocialNetwork.EF.Repo;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using SocialNetwork.Util;

namespace SocialNetwork.Nucleus
{
    public class CurrentAppUser
    {
        public class Query : IRequest<UserDto> { }

        public class Handler : IRequestHandler<Query, UserDto>
        {
            #region Members
            private readonly IUnitOfWork _unitOfWork;
            private readonly IUserAccessor _userAccessor;
            private readonly IMapperHelper _mapperHelper;
            #endregion


            #region Constructor
            public Handler(IUnitOfWork unitOfWork, IUserAccessor userAccessor, IMapperHelper mapperHelper)
            {
                _unitOfWork = unitOfWork;
                _userAccessor = userAccessor;
                _mapperHelper = mapperHelper;
            }
            #endregion


            #region Public Methods
            public async Task<UserDto> Handle(Query query, CancellationToken cancellationToken)
            {
                Guid userId = _userAccessor.GetCurrentUserId();
                string userName = _userAccessor.GetCurrentUserName();
                AppUser user = await _unitOfWork.AppUserRepo.FindFirstAsync(e => e.Id == userId,
                                    new List<string> { nameof(AppUser.Photos) },
                                    cancellationToken);

                return _mapperHelper.Map<AppUser, UserDto>(user);
            }
            #endregion
        }
    }
}
