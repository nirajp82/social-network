namespace SocialNetwork.Util
{
    public class UtilFactory
    {
        #region Members
        public ICryptoHelper CryptoHelper { get; }
        public IMapperHelper MapperHelper { get; }
        public AppConfigHelper AppConfigHelper { get; }
        #endregion


        #region Constructor
        public UtilFactory(ICryptoHelper cryptoHelper, AppConfigHelper appConfigHelper, IMapperHelper mapperHelper)
        {
            CryptoHelper = cryptoHelper;
            AppConfigHelper = appConfigHelper;
            MapperHelper = mapperHelper;
        }
        #endregion
    }
}
