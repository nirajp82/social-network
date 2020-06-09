using MediatR;
using SocialNetwork.EF.Repo;
using SocialNetwork.Nucleus.Helper;
using SocialNetwork.Util;
using System;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace SocialNetwork.Nucleus.Engine.Activity
{
    public class Edit
    {
        public class EditCommand : IRequest
        {
            [JsonIgnore]
            public Guid Id { get; set; }

            public string Title { get; set; }

            public string Description { get; set; }

            public string Category { get; set; }

            public DateTime? Date { get; set; }

            public string City { get; set; }

            public string Venue { get; set; }
        }

        public class EditHandler : IRequestHandler<EditCommand>
        {
            #region Members
            private IUnitOfWork _unitOfWork { get; }
            private IMapperHelper _mapperHelper { get; }
            #endregion


            #region Constuctor
            public EditHandler(IUnitOfWork unitOfWork, IMapperHelper mapperHelper)
            {
                _unitOfWork = unitOfWork;
                _mapperHelper = mapperHelper;
            }
            #endregion


            #region Methods
            public async Task<Unit> Handle(EditCommand request, CancellationToken cancellationToken)
            {
                //Get existing activity from database
                DataModel.Activity dbActivity = await _unitOfWork.ActivityRepo.FindFirstAsync(request.Id, cancellationToken);

                //Keep existing value as if user is not passing it from front end
                request.Category ??= dbActivity.Category;
                request.City ??= dbActivity.City;
                request.Date ??= dbActivity.Date;
                request.Description ??= dbActivity.Description;
                request.Title ??= dbActivity.Title;
                request.Venue ??= dbActivity.Venue;

                DataModel.Activity activity = _mapperHelper.Map<EditCommand, DataModel.Activity>(request);

                _unitOfWork.ActivityRepo.Update(activity);

                int cnt = await _unitOfWork.SaveAsync(cancellationToken);
                if (cnt > 0)
                    return Unit.Value;

                throw new Exception("Problem saving changes to database");
            }
            #endregion
        }
    }
}