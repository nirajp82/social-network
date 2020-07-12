using MediatR;
using SocialNetwork.EF.Repo;
using SocialNetwork.Util;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace SocialNetwork.Nucleus.Photo
{
    public class Delete
    {
        public class Command : IRequest
        {
            public Guid PhotoId { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            #region Members
            private readonly IPhotoAccessor _photoAccessor;
            private IUnitOfWork _unitOfWork { get; }
            #endregion


            #region Constuctor
            public Handler(IPhotoAccessor photoAccessor, IUnitOfWork unitOfWork)
            {
                _photoAccessor = photoAccessor;
                _unitOfWork = unitOfWork;
            }
            #endregion


            #region Methods
            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                DataModel.Photo photo = await _unitOfWork.PhotoRepo.FindFirstAsync(request.PhotoId, cancellationToken);
                if (photo == null)
                    throw new CustomException(HttpStatusCode.NotFound, new { PhotoId = "Invalid photo id" });

                await _photoAccessor.DeletePhotoAsync(photo.CloudFileName, cancellationToken);
                _unitOfWork.PhotoRepo.Delete(photo);
                int cnt = await _unitOfWork.SaveAsync(cancellationToken);

                if (cnt > 0)
                    return Unit.Value;

                throw new Exception("Problem saving changes to database");
            }
            #endregion
        }
    }
}
