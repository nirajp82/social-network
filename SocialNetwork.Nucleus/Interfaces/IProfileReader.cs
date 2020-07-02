using SocialNetwork.DataModel;
using SocialNetwork.Dto;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SocialNetwork.Nucleus
{
    public interface IProfileReader
    {
        Task<ProfileDto> ReadProfile(Guid appUserId, CancellationToken cancellationToken);
    }
}
