using MediatR;
using SocialNetwork.DataModel;
using SocialNetwork.EF.Repo;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SocialNetwork.Nucleus.Engine.Activities
{
    public class Exists
    {
        public class Query : IRequest<bool>
        {
            public Guid Id { get; set; }
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
                bool result = await _unitOfWork.ActivityRepository.ExistsAsync(request.Id, cancellationToken);
                return result;
            }
            #endregion
        }
    }
}