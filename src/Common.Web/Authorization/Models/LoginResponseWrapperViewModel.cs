using System.Text.Json.Serialization;

namespace StatementIQ.Common.Web.Authorization.Models
{
    internal class LoginResponseWrapperViewModel
    {
        [JsonPropertyName("token")] public string Token { get; set; }
    }
}