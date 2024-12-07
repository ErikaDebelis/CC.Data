
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace CC.Data.Identity.Models
{
    public enum Pronoun
    {
        [JsonProperty("He")]
        [JsonPropertyName("He")]
        He,
        [JsonProperty("Him")]
        [JsonPropertyName("Him")]
        Him,
        [JsonProperty("Her")]
        [JsonPropertyName("Her")]
        Her,
        [JsonProperty("She")]
        [JsonPropertyName("She")]
        She,
        [JsonProperty("Them")]
        [JsonPropertyName("Them")]
        Them,
        [JsonProperty("They")]
        [JsonPropertyName("They")]
        They
    }
}