using MediatR;
using SocialNetwork.Dto;
using SocialNetwork.EF.Repo;
using SocialNetwork.Util;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SocialNetwork.Nucleus.Comment
{
    public class List
    {
        public class Command : IRequest<IEnumerable<CommentDto>>
        {
            public Guid ActivityId { get; set; }
        }

        public class Handler : IRequestHandler<Command, IEnumerable<CommentDto>>
        {
            #region Members
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapperHelper _mapperHelper;
            private readonly IPhotoAccessor _photoAccessor;
            #endregion


            #region Constuctor
            public Handler(IUnitOfWork unitOfWork, IMapperHelper mapperHelper, IPhotoAccessor photoAccessor)
            {
                _unitOfWork = unitOfWork;
                _mapperHelper = mapperHelper;
                _photoAccessor = photoAccessor;
            }
            #endregion


            #region Methods
            public async Task<IEnumerable<CommentDto>> Handle(Command request, CancellationToken cancellationToken)
            {
                IEnumerable<DataModel.Comment> dbComments = await _unitOfWork.CommentRepo.FindAsync(request.ActivityId, cancellationToken);
                IEnumerable<CommentDto> comments = _mapperHelper.MapList<DataModel.Comment, CommentDto>(dbComments);
                if (comments != null)
                {
                    foreach (var comment in comments)
                    {
                        comment.UserImage = _photoAccessor.PreparePhotoUrl(comment.MainPhotoCloudFileName);
                    }
                }
                return comments;
            }
            #endregion
        }
    }
}
