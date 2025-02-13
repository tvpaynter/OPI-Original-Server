using System.Text.Json.Serialization;

namespace Utg.Api.Models
{
    public class Contact
    {
        [JsonPropertyName("type")] public ContactType Type { get; set; }

        [JsonPropertyName("firstName")] public string FirstName { get; set; }

        [JsonPropertyName("lastName")] public string LastName { get; set; }

        [JsonPropertyName("Company")] public string Company { get; set; }

        [JsonPropertyName("address")] public Address Address { get; set; }

        [JsonPropertyName("phoneNumber")] public string PhoneNumber { get; set; }

        [JsonPropertyName("emailAddress")] public string EmailAddress { get; set; }
    }
}