
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace CC.Data.Identity.Models
{
    public enum ProfileRole
    {
        [JsonProperty("Owner")]
        [JsonPropertyName("Owner")]
        Owner,
        [JsonProperty("Admin")]
        [JsonPropertyName("Admin")]
        Admin,
        [JsonProperty("Custom")]
        [JsonPropertyName("Custom")]
        Custom
    }
}