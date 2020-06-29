using SocialNetwork.DataModel;
using System;
using System.Collections.Generic;
using System.Text;

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
        #endregion
    }
}
