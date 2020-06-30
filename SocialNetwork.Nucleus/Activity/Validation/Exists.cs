using MediatR;
using SocialNetwork.EF.Repo;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SocialNetwork.Nucleus.Engine.Activity
{
    public class Exists
    {
        public class Query : IRequest<bool>
        {
            public Guid ActivityId { get; set; }
        }

        public class Handler : IRequestHandler<Query, bool>
        {
            #region Members
            private IUnitOfWork _unitOfWork { get; }
            #endregion


            #region Constuctor
            public Handler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }
            #endregion


            #region Methods
            public async Task<bool> Handle(Query request, CancellationToken cancellationToken)
            {
                bool result = await _unitOfWork.ActivityRepo.ExistsAsync(request.ActivityId, cancellationToken);
                return result;
            }
            #endregion
        }
    }
}