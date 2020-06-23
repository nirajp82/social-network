using MediatR;
using Microsoft.AspNetCore.Http;
using SocialNetwork.DataModel;
using SocialNetwork.Dto;
using SocialNetwork.EF.Repo;
using SocialNetwork.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SocialNetwork.Nucleus.Engine.Photo
{
    public class SetMain
    {
        public class Command : IRequest
        {
            public Guid PhotoId { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            #region Members
            private readonly IUnitOfWork _unitOfWork;
            private readonly IUserAccessor _userAccessor;
            #endregion


            #region Constructor
            public Handler(IUnitOfWork unitOfWork, IUserAccessor userAccessor)
            {
                _unitOfWork = unitOfWork;
                _userAccessor = userAccessor;
            }
            #endregion


            #region Methods
            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                AppUser appUser = await _unitOfWork.AppUserRepo.FindByUserName(_userAccessor.GetCurrentUserName(), cancellationToken);
                IEnumerable<DataModel.Photo> photos = await _unitOfWork.PhotoRepo.FindMainPhotosAsync(appUser.Id, request.PhotoId, cancellationToken);

                if (!photos.Any(p => p.Id == request.PhotoId))
                    throw new CustomException(System.Net.HttpStatusCode.NotFound, new { Photo = "Not found" });

                foreach (var photo in photos)
                    photo.IsMainPhoto = photo.Id == request.PhotoId;

                _unitOfWork.PhotoRepo.Update(photos);

                int insertCnt = await _unitOfWork.SaveAsync(cancellationToken);
                if (insertCnt > 0)
                    return Unit.Value;

                throw new Exception("Problem saving uploaded file entry into database.");
            }
            #endregion
        }
    }
}
