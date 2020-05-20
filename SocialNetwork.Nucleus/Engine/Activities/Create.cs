using MediatR;
using SocialNetwork.DataModel;
using SocialNetwork.EF.Repo;
using SocialNetwork.Nucleus.Helper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SocialNetwork.Nucleus.Engine.Activities
{
    public class Create
    {
        public class Command : IRequest<Guid>
        {
            public string Title { get; set; }

            public string Description { get; set; }

            public string Category { get; set; }

            public DateTime Date { get; set; }

            public string City { get; set; }

            public string Venue { get; set; }
        }

        public class Handler : IRequestHandler<Command, Guid>
        {
            #region Members
            private IUnitOfWork _unitOfWork { get; }
            private IMapperHelper _mapperHelper { get; }
            #endregion


            #region Constuctor
            public Handler(IUnitOfWork unitOfWork, IMapperHelper mapperHelper)
            {
                _unitOfWork = unitOfWork;
                _mapperHelper = mapperHelper;
            }
            #endregion


            #region Methods
            public async Task<Guid> Handle(Command request, CancellationToken cancellationToken)
            {
                Activity activity = _mapperHelper.Map<Command, Activity>(request);
                //Generate new Id for new Entity
                activity.Id = Guid.NewGuid();
                _unitOfWork.ActivityRepository.Add(activity);

                int insertCnt = await _unitOfWork.SaveAsync(cancellationToken);
                if (insertCnt > 0)
                    return activity.Id;

                throw new Exception("Problem saving changes to database");
            }
            #endregion
        }
    }
}
