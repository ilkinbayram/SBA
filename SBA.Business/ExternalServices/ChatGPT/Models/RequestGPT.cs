using System.Text.Json.Serialization;

namespace SBA.Business.ExternalServices.ChatGPT.Models
{
    public class RequestGPT
    {
        [JsonPropertyName("model")]
        public string Model { get; set; } = "gpt-3.5-turbo";

        [JsonPropertyName("max_tokens")]
        public int MaxTokens { get; set; } = 500;

        [JsonPropertyName("messages")]
        public RequestMessageGPT[] Messages { get; set; }
    }
}
