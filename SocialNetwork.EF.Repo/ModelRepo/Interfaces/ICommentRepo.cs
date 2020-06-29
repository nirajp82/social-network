using SocialNetwork.DataModel;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SocialNetwork.EF.Repo
{
    public interface ICommentRepo
    {
        void Add(Comment entity);
    }
}
