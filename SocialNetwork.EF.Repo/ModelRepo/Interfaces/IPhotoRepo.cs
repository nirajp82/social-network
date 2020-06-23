using SocialNetwork.DataModel;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SocialNetwork.EF.Repo
{
    public interface IPhotoRepo
    {
        void Add(Photo entity);
        Task<Photo> FindFirstAsync(Guid photoId, CancellationToken cancellationToken);
        Task<IEnumerable<Photo>> FindMainPhotosAsync(Guid appUserId, Guid photoId, CancellationToken cancellationToken);

        void Update(IEnumerable<Photo> photos);

        void Delete(Photo entity);
    }
}