using FluentValidation;
using MediatR;
using SocialNetwork.DataModel;
using SocialNetwork.EF.Repo;
using SocialNetwork.Nucleus.Helper;
using SocialNetwork.Util;
using System;
using System.Net;
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

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(c => c.Title).NotEmpty();
                RuleFor(c => c.Description).NotEmpty();
                RuleFor(c => c.Category).NotEmpty();
                RuleFor(c => c.Date).NotEmpty();
                RuleFor(c => c.City).NotEmpty();
                RuleFor(c => c.Venue).NotEmpty();
            }
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

                throw new CustomException(HttpStatusCode.InternalServerError,
                            new { CreateActivity = "Problem saving changes to database" });
            }
            #endregion
        }
    }
}
