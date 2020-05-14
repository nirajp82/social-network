using System.Threading.Tasks;

namespace SocialNetwork.EF.Repo
{
    public interface IUnitOfWork
    {
        IValueRepository ValueRepository { get; }

        Task<int> SaveAsync();
    }
}
