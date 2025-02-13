
using System;
using System.Text.Json.Serialization;

namespace Utg.Api.Models
{
    public class LoginResponseUserViewModel
    {
        [JsonPropertyName("userId")] public long UserId { get; set; }

        [JsonPropertyName("name")] public string Name { get; set; }

        [JsonPropertyName("birthday")] public object Birthday { get; set; }

        [JsonPropertyName("flags")] public long Flags { get; set; }

        [JsonPropertyName("lastPasswordChangedAt")]
        public DateTimeOffset LastPasswordChangedAt { get; set; }

        [JsonPropertyName("lastLoginAt")] public DateTimeOffset LastLoginAt { get; set; }

        [JsonPropertyName("joined")] public DateTimeOffset Joined { get; set; }

        [JsonPropertyName("passwordFailuresSinceLastSuccess")]
        public long PasswordFailuresSinceLastSuccess { get; set; }

        [JsonPropertyName("lastPasswordFailureAt")]
        public object LastPasswordFailureAt { get; set; }

        [JsonPropertyName("deletedAt")] public object DeletedAt { get; set; }

        [JsonPropertyName("loginCount")] public long LoginCount { get; set; }

        [JsonPropertyName("groups")] public Features Groups { get; set; }

        [JsonPropertyName("features")] public Features Features { get; set; }
    }
}