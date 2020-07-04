using AutoMapper;
using SocialNetwork.Dto;
using SocialNetwork.DataModel;
using SocialNetwork.Nucleus.Engine.Activity;
using SocialNetwork.Nucleus.Engine.Photo;

namespace SocialNetwork.Nucleus
{
    internal abstract class BaseMapper : Profile
    {
        #region Constructor
        public BaseMapper()
        {
            Map<string, string>().ConvertUsing(str => string.IsNullOrWhiteSpace(str) ? str : str.Trim());
        } 
        #endregion

        #region Methods
        public IMappingExpression<source, dest> Map<source, dest>(bool createReserseMap = true)
        {
            if (createReserseMap)
                return CreateMap<dest, source>().ReverseMap();

            return CreateMap<source, dest>();
        }
        #endregion       
    }
}
