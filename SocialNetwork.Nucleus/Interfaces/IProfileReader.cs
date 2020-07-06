using SocialNetwork.Dto;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SocialNetwork.Nucleus
{
    public interface IProfileReader
    {
        Task<ProfileDto> ReadProfile(Guid appUserId, CancellationToken cancellationToken);
    }
}
