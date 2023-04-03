using System.Text.Json.Serialization;

namespace SBA.Business.ExternalServices.ChatGPT.Models
{
    public class RequestMessageGPT
    {
        [JsonPropertyName("role")]
        public string Role { get; set; }

        [JsonPropertyName("content")]
        public string Content { get; set; }
    }
}
