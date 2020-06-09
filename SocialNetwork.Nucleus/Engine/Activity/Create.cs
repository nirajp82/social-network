using FluentValidation;
using MediatR;
using SocialNetwork.EF.Repo;
using SocialNetwork.Nucleus.Helper;
using SocialNetwork.Util;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SocialNetwork.Nucleus.Engine.Activity
{
    public class Create
    {
        public class CreateCommand : IRequest<Guid>
        {
            public string Title { get; set; }

            public string Description { get; set; }

            public string Category { get; set; }

            public DateTime Date { get; set; }

            public string City { get; set; }

            public string Venue { get; set; }
        }

        public class CreateCommandValidator : AbstractValidator<CreateCommand>
        {
            public CreateCommandValidator()
            {
                RuleFor(c => c.Title).NotEmpty();
                RuleFor(c => c.Description).NotEmpty();
                RuleFor(c => c.Category).NotEmpty();
                RuleFor(c => c.Date).NotEmpty();
                RuleFor(c => c.City).NotEmpty();
                RuleFor(c => c.Venue).NotEmpty();
            }
        }

        public class CreateHandler : IRequestHandler<CreateCommand, Guid>
        {
            #region Members
            private IUnitOfWork _unitOfWork { get; }
            private IMapperHelper _mapperHelper { get; }
            #endregion


            #region Constuctor
            public CreateHandler(IUnitOfWork unitOfWork, IMapperHelper mapperHelper)
            {
                _unitOfWork = unitOfWork;
                _mapperHelper = mapperHelper;
            }
            #endregion


            #region Methods
            public async Task<Guid> Handle(CreateCommand request, CancellationToken cancellationToken)
            {
                DataModel.Activity activity = _mapperHelper.Map<CreateCommand, DataModel.Activity>(request);
                //Generate new Id for new Entity
                activity.Id = Guid.NewGuid();
                _unitOfWork.ActivityRepo.Add(activity);

                int insertCnt = await _unitOfWork.SaveAsync(cancellationToken);
                if (insertCnt > 0)
                    return activity.Id;

                throw new Exception("Problem saving changes to database");
            }
            #endregion
        }
    }
}
