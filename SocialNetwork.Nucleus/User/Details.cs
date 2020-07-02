using MediatR;
using SocialNetwork.Dto;
using SocialNetwork.Util;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SocialNetwork.Nucleus.Engine.User
{
    public class Details
    {
        public class Query : IRequest<ProfileDto>
        {
            public Guid AppUserId { get; set; }
        }

        public class Handler : IRequestHandler<Query, ProfileDto>
        {
            #region Members
            private readonly IProfileReader _profileReader;
            #endregion


            #region Constructor
            public Handler(IProfileReader profileReader)
            {
                _profileReader = profileReader;
            }
            #endregion


            #region Methods
            public async Task<ProfileDto> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _profileReader.ReadProfile(request.AppUserId, cancellationToken);
            }
            #endregion
        }
    }
}
