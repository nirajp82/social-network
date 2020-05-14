using AutoMapper;
using SocialNetwork.APIEntity;
using SocialNetwork.DataModel;

namespace SocialNetwork.Nucleus.Helper
{
    internal class MapperRegistry : Profile
    {
        #region Constructor
        public MapperRegistry()
        {
            Map<string, string>().ConvertUsing(str => string.IsNullOrWhiteSpace(str) ? str : str.Trim());
            Map<Value, ValueEntity>();
            Map<ValueEntity, Value>();
        }
        #endregion


        #region Private Methods
        IMappingExpression<source, dest> Map<source, dest>() => CreateMap<source, dest>();
        #endregion
    }
}
