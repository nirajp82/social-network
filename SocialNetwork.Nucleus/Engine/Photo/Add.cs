using MediatR;
using Microsoft.AspNetCore.Http;
using SocialNetwork.DataModel;
using SocialNetwork.Dto;
using SocialNetwork.EF.Repo;
using SocialNetwork.Util;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SocialNetwork.Nucleus.Engine.Photo
{
    public class Add
    {
        public class Command : IRequest<string>
        {
            public bool IsMainPhoto { get; set; }

            public IFormFile File { get; set; }
        }

        public class Handler : IRequestHandler<Command, string>
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
            public async Task<string> Handle(Command request, CancellationToken cancellationToken)
            {
                PhotoDto photoUploadResult = await _photoAccessor.AddPhotoAsync(request.File, cancellationToken);
                string userName = _userAccessor.GetCurrentUserName();

                AppUser user = await _unitOfWork.AppUserRepo.FindFirstAsync(e => e.IdentityUser.UserName == userName);

                DataModel.Photo photo = _mapperHelper.Map<Command, DataModel.Photo>(request);
                photo.Id = photoUploadResult.Id;
                photo.UploadedDate = DateTime.Now;
                photo.AppUserId = user.Id;
                _unitOfWork.PhotoRepo.Add(photo);
                int insertCnt = await _unitOfWork.SaveAsync(cancellationToken);
                if (insertCnt > 0)
                    return photoUploadResult.Url;

                throw new Exception("Problem saving uploaded file entry into database.");
            }
            #endregion
        }
    }
}
