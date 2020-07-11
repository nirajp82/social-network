using System.Text.Json.Serialization;

namespace SocialNetwork.Infrastructure
{
    internal class FacebookUserAccessTokenData
    {
        [JsonPropertyName("app_id")]
        public long AppId { get; set; }
        public string Type { get; set; }
        public string Application { get; set; }

        [JsonPropertyName("expires_at")]
        public long ExpiresAt { get; set; }

        [JsonPropertyName("is_valid")]
        public bool IsValid { get; set; }

        [JsonPropertyName("user_id")]
        public long UserId { get; set; }
    }
}