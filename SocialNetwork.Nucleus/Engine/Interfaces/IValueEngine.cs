using SocialNetwork.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SocialNetwork.Nucleus
{
    public interface IValueEngine
    {
        Task<IEnumerable<ValueDto>> FindAllAsync();

        Task<ValueDto> AddAsync(ValueDto entity);

        Task UpdateAsync(ValueDto entity);

        Task DeleteAsync(long valueId);

        Task<ValueDto> FindFirstAsync(long valueId);
    }
}
