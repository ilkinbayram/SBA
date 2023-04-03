using System.Text.Json.Serialization;

namespace SBA.Business.ExternalServices.ChatGPT.Models
{
    public class ResponseMessageGPT
    {
        [JsonPropertyName("role")]
        public string Role { get; set; }

        [JsonPropertyName("content")]
        public string Content { get; set; }
    }
}
