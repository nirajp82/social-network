using SocialNetwork.DataModel;

namespace SocialNetwork.EF.Repo
{
    public interface IUserActivityRepo
    {
        void Add(UserActivity entity);
    }
}
