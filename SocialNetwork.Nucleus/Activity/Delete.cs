using MediatR;
using SocialNetwork.EF.Repo;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SocialNetwork.Nucleus.Activity
{
    public class Delete
    {
        public class Command : IRequest
        {
            public Guid ActivityId { get; set; }
        }

        public class Handler : IRequestHandler<Command>
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
            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                await _unitOfWork.ActivityRepo.DeleteAsync(request.ActivityId, cancellationToken);

                int cnt = await _unitOfWork.SaveAsync(cancellationToken);
                if (cnt > 0)
                    return Unit.Value;

                throw new Exception("Problem saving changes to database");
            }
            #endregion
        }
    }
}
