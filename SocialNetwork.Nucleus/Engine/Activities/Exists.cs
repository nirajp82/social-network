using MediatR;
using SocialNetwork.EF.Repo;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SocialNetwork.Nucleus.Engine.Activities
{
    public class Exists
    {
        public class ExistsQuery : IRequest<bool>
        {
            public Guid Id { get; set; }
        }

        public class ExistsHandler : IRequestHandler<ExistsQuery, bool>
        {
            #region Members
            private IUnitOfWork _unitOfWork { get; }
            #endregion


            #region Constuctor
            public ExistsHandler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }
            #endregion


            #region Methods
            public async Task<bool> Handle(ExistsQuery request, CancellationToken cancellationToken)
            {
                bool result = await _unitOfWork.ActivityRepository.ExistsAsync(request.Id, cancellationToken);
                return result;
            }
            #endregion
        }
    }
}