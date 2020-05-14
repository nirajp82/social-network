using SocialNetwork.DataModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SocialNetwork.EF.Repo
{
    public interface IValueRepository
    {
        Task<IEnumerable<Value>> FindAllAsync();

        void Add(Value entity);

        void Update(Value entity);

        Task DeleteAsync(long valueId);

        Task<Value> FindFirstAsync(long valueId);
    }
}
