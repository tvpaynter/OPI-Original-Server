using System.Text.Json.Serialization;

namespace Utg.Api.Models
{
    public class LoginResponseWrapperViewModel
    {
        [JsonPropertyName("user")] public LoginResponseUserViewModel User { get; set; }

        [JsonPropertyName("token")] public string Token { get; set; }
    }
}