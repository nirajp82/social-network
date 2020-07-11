using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using SocialNetwork.Dto;
using SocialNetwork.Nucleus;
using System.Text.Json;

namespace SocialNetwork.Infrastructure
{
    internal class FacebookAccessor : IFacebookAccessor
    {
        #region Members
        private const string _facebookGraph = "https://graph.facebook.com/";
        private readonly HttpClient _httpClient;
        private readonly ConfigSettings _config;
        #endregion

        #region Constructor
        public FacebookAccessor(ConfigSettings config)
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(_facebookGraph)
            };
            _httpClient.DefaultRequestHeaders
                .Accept
                .Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _config = config;
        }
        #endregion


        #region Public Methods
        public async Task<FacebookUserDto> FacebookLogin(string accessToken)
        {
            FacebookAppAccessToken appAccessToken = await GetData<FacebookAppAccessToken>(
                $"/oauth/access_token?client_id={_config.FacebookAppId}" +
                "&client_secret={_config.FacebookAppSecret}&grant_type=client_credentials");

            if (appAccessToken == null)
                return null;

            FacebookUserAccessTokenValidation userAccessTokenValidation = await GetData<FacebookUserAccessTokenValidation>(
                $"debug_token?input_token={accessToken}&access_token={appAccessToken.AccessToken}");

            if (userAccessTokenValidation != null && !userAccessTokenValidation.Data.IsValid)
                return null;

            return await GetData<FacebookUserDto>($"me?fields=id,email,first_name,picture&access_token={accessToken}");
        }
        #endregion


        #region Private Methods
        private async Task<T> GetData<T>(string uri)
        {
            string response = await _httpClient.GetStringAsync(uri);
            if (!string.IsNullOrWhiteSpace(response))
                return JsonSerializer.Deserialize<T>(response);
            return default;
        }
        #endregion
    }
}