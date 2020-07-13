using MediatR;
using Microsoft.AspNetCore.Http;
using SocialNetwork.Dto;
using SocialNetwork.EF.Repo;
using SocialNetwork.Util;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SocialNetwork.Nucleus.Photo
{
    public class Add
    {
        public class Command : IRequest<PhotoDto>
        {
            public IFormFile File { get; set; }
        }

        public class Handler : IRequestHandler<Command, PhotoDto>
        {
            #region Members
            private readonly IPhotoAccessor _photoAccessor;
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapperHelper _mapperHelper;
            private readonly IUserAccessor _userAccessor;
            #endregion


            #region Constructor
            public Handler(IPhotoAccessor photoAccessor, IUnitOfWork unitOfWork,
                IMapperHelper mapperHelper, IUserAccessor userAccessor)
            {
                _photoAccessor = photoAccessor;
                _unitOfWork = unitOfWork;
                _mapperHelper = mapperHelper;
                _userAccessor = userAccessor;
            }
            #endregion


            #region Methods
            public async Task<PhotoDto> Handle(Command request, CancellationToken cancellationToken)
            {
                PhotoDto photoUploadResult = await _photoAccessor.AddPhotoAsync(request.File, cancellationToken);
                DataModel.Photo photo = CreatePhotoModel(request, photoUploadResult);
                _unitOfWork.PhotoRepo.Add(photo);
                int insertCnt = await _unitOfWork.SaveAsync(cancellationToken);
                if (insertCnt > 0)
                {
                    photoUploadResult.Id = photo.Id;
                    return photoUploadResult;
                }
                throw new Exception("Problem saving uploaded file entry into database.");
            }

            private DataModel.Photo CreatePhotoModel(Command request, PhotoDto photoUploadResult)
            {
                //TODO: Move to Automapper
                DataModel.Photo photo = _mapperHelper.Map<Command, DataModel.Photo>(request);
                photo.Id = photoUploadResult.Id;
                photo.UploadedDate = DateTime.Now;
                photo.AppUserId = _userAccessor.GetCurrentUserId();
                photo.CloudFileName = photoUploadResult.CloudFileName;
                return photo;
            }
            #endregion
        }
    }
}
