using SocialNetwork.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SocialNetwork.Nucleus.Interfaces
{
    public interface IUserActivityHelper
    {
        Task<IEnumerable<ActivityDto>>  PrepareActivities(IEnumerable<DataModel.Activity> activities);
        Task<ActivityDto> PrepareActivity(DataModel.Activity activities);
    }
}
