using MediatR;
using SocialNetwork.DataModel;
using SocialNetwork.EF.Repo;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SocialNetwork.Nucleus.Engine.Activity
{
    public class IsHost
    {
        public class Query : IRequest<bool>
        {
            public Guid ActivityId { get; set; }
        }

        public class Handler : IRequestHandler<Query, bool>
        {
            #region Members
            private readonly IUnitOfWork _unitOfWork;
            private readonly IUserAccessor _userAccessor;
            #endregion


            #region Constuctor
            public Handler(IUnitOfWork unitOfWork, IUserAccessor userAccessor)
            {
                _unitOfWork = unitOfWork;
                _userAccessor = userAccessor;
            }
            #endregion


            #region Methods
            public async Task<bool> Handle(Query request, CancellationToken cancellationToken)
            {
                AppUser appUser = await _unitOfWork.AppUserRepo.FindByUserName(_userAccessor.GetCurrentUserName());
                return await _unitOfWork.UserActivityRepo.IsHostAsync(request.ActivityId, appUser.Id, cancellationToken);
            }
            #endregion
        }
    }
}