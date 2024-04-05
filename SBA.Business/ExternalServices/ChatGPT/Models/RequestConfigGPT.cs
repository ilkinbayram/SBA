using System.Text.Json.Serialization;

namespace SBA.Business.ExternalServices.ChatGPT.Models
{
    public class RequestConfigGPT
    {
        public RequestConfigGPT()
        {
        }

        public RequestConfigGPT(int maxTokens, float temperature)
        {
            MaxTokens = maxTokens;
            Temperature = temperature;
        }

        [JsonPropertyName("max_tokens")]
        public int MaxTokens { get; set; } = 500;

        [JsonPropertyName("temperature")]
        public float Temperature { get; set; } = (float)0.1;
    }
}
