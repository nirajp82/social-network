using System.Text.Json.Serialization;

namespace SocialNetwork.Infrastructure
{
    internal class FacebookAppAccessToken
    {
        [JsonPropertyName("token_type")]
        public string TokenType { get; set; }

        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; }
    }
}