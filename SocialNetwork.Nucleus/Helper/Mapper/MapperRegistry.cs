using AutoMapper;
using SocialNetwork.DTO;
using SocialNetwork.DataModel;
using SocialNetwork.Nucleus.Engine.Activity;
using SocialNetwork.Nucleus.Engine.User;

namespace SocialNetwork.Nucleus.Helper
{
    internal class MapperRegistry : Profile
    {
        #region Constructor
        public MapperRegistry()
        {
            Map<string, string>().ConvertUsing(str => string.IsNullOrWhiteSpace(str) ? str : str.Trim());
            Map<Value, ValueDTO>();

            Map<Activity, ActivityDTO>();
            Map<Create.Command, Activity>();
            Map<Edit.Command, Activity>();
        }
        #endregion


        #region Private Methods
        IMappingExpression<source, dest> Map<source, dest>(bool createReserseMap = true)
        {
            if (createReserseMap)
                return CreateMap<dest, source>().ReverseMap();

            return CreateMap<source, dest>();
        }
        #endregion
    }
}
