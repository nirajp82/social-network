using Microsoft.EntityFrameworkCore;
using SocialNetwork.DataModel;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SocialNetwork.EF.Repo
{
    internal class PhotoRepo : RepositoryBase<Photo>, IPhotoRepo
    {
        #region Constructor
        public PhotoRepo(ApplicationContext context) : base(context)
        {
        }
        #endregion


        #region Public Methods
        public Task<Photo> FindFirstAsync(Guid photoId, CancellationToken cancellationToken)
        {
            return base.FindFirstAsync((p => p.Id == photoId), null, cancellationToken);
        }

        public async Task<IEnumerable<Photo>> FindMainPhotosAsync(Guid appUserId, Guid photoId, CancellationToken cancellationToken)
        {
            IEnumerable<Photo> photos = await base.Find(p => p.AppUserId == appUserId && (p.Id == photoId || p.IsMainPhoto))
                                        .ToListAsync(cancellationToken);
            return photos;
        }

        public void Update(IEnumerable<Photo> photos)
        {
            foreach (var entity in photos)
                base.Update(entity);
        }
        #endregion
    }
}
