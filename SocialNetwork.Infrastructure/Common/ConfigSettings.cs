namespace SocialNetwork.Infrastructure
{
    public class ConfigSettings
    {
        #region Members
        public string BlobAccountBaseUri { get; private set; }
        public string AccountName { get; private set; }
        public string AccountKey { get; private set; }
        public string UserImageContainerName { get; private set; }
        public string UserImageContainerPath { get; private set; }
        public string DataProtectionKey { get; private set; }
        #endregion


        #region Constructor
        public ConfigSettings(Util.AppConfigHelper configHelper)
        {
            BlobAccountBaseUri = configHelper.GetValue<string>("Azure:Storage:BlobAccountBaseUri");
            AccountName = configHelper.GetValue<string>("Azure:Storage:AccountName");
            AccountKey = configHelper.GetValue<string>("Azure:Storage:AccountKey");
            UserImageContainerName = configHelper.GetValue<string>("Azure:Storage:Containers:UserImagesName");
            UserImageContainerPath = $"{BlobAccountBaseUri}/{UserImageContainerName}";

            DataProtectionKey = configHelper.GetValue<string>("Security:DataProtectionKey");
        }
        #endregion
    }
}
