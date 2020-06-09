using MediatR;
using SocialNetwork.EF.Repo;
using SocialNetwork.Nucleus.Helper;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SocialNetwork.Nucleus.Engine.Activities
{
    public class Delete
    {
        public class DeleteCommand : IRequest
        {
            public Guid Id { get; set; }
        }

        public class DeleteHandler : IRequestHandler<DeleteCommand>
        {
            #region Members
            private IUnitOfWork _unitOfWork { get; }
            private IMapperHelper _mapperHelper { get; }
            #endregion


            #region Constuctor
            public DeleteHandler(IUnitOfWork unitOfWork, IMapperHelper mapperHelper)
            {
                _unitOfWork = unitOfWork;
                _mapperHelper = mapperHelper;
            }
            #endregion


            #region Methods
            public async Task<Unit> Handle(DeleteCommand request, CancellationToken cancellationToken)
            {
                await _unitOfWork.ActivityRepository.DeleteAsync(request.Id, cancellationToken);

                int cnt = await _unitOfWork.SaveAsync(cancellationToken);
                if (cnt > 0)
                    return Unit.Value;

                throw new Exception("Problem saving changes to database");
            }
            #endregion
        }
    }
}
