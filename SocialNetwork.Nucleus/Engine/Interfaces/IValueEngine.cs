using SocialNetwork.APIEntity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SocialNetwork.Nucleus
{
    public interface IValueEngine
    {
        Task<IEnumerable<ValueEntity>> FindAllAsync();

        Task<ValueEntity> AddAsync(ValueEntity entity);

        Task UpdateAsync(ValueEntity entity);

        Task DeleteAsync(long valueId);

        Task<ValueEntity> FindFirstAsync(long valueId);
    }
}
