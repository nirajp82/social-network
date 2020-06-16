using SocialNetwork.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SocialNetwork.Nucleus
{
    public interface IValueEngine
    {
        Task<IEnumerable<ValueDTO>> FindAllAsync();

        Task<ValueDTO> AddAsync(ValueDTO entity);

        Task UpdateAsync(ValueDTO entity);

        Task DeleteAsync(long valueId);

        Task<ValueDTO> FindFirstAsync(long valueId);
    }
}
