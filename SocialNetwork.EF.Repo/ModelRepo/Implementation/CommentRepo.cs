using Microsoft.EntityFrameworkCore;
using SocialNetwork.DataModel;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SocialNetwork.EF.Repo.ModelRepo.Implementation
{
    internal class CommentRepo : RepositoryBase<Comment>, ICommentRepo
    {
        #region Constructor
        public CommentRepo(ApplicationContext context) : base(context)
        {
        }
        #endregion


        #region Public Method
        public async Task<IEnumerable<Comment>> FindAsync(Guid activityId, CancellationToken cancellationToken)
        {
            return await base.Find(e => e.ActivityId == activityId, null)
                            .Include(e => e.Author)
                            .ThenInclude(e => e.Photos)
                            .ToListAsync(cancellationToken);
        }
        #endregion
    }
}
