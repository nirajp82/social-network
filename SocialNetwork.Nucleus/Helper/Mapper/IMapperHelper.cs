using System.Collections.Generic;

namespace SocialNetwork.Nucleus.Helper
{
    public interface IMapperHelper
    {
        dest Map<src, dest>(src entity);

        IEnumerable<dest> MapList<src, dest>(IEnumerable<src> list);
    }
}
