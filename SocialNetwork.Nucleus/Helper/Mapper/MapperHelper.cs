using AutoMapper;
using System.Collections.Generic;
using System.Linq;

namespace SocialNetwork.Nucleus.Helper
{
    internal class MapperHelper : IMapperHelper
    {
        #region Private Members
        private IMapper _mapper { get; }
        #endregion


        #region Constructor
        public MapperHelper(IMapper mapper)
        {
            _mapper = mapper;
        }
        #endregion


        #region Public Methods
        public dest Map<src, dest>(src entity)
        {
            if (entity != null)
                return _mapper.Map<dest>(entity);

            return default;
        }


        public IEnumerable<dest> MapList<src, dest>(IEnumerable<src> list)
        {
            if (list?.Any() == true)
                return _mapper.Map<IEnumerable<dest>>(list);

            return default;
        }
        #endregion
    }
}