using System.Text.Json.Serialization;

namespace SBA.Business.ExternalServices.ChatGPT.Models
{
    public class ResponseGPT
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("created")]
        public int Created { get; set; }

        [JsonPropertyName("model")]
        public string Model { get; set; }

        [JsonPropertyName("usage")]
        public ResponseUsageGPT Usage { get; set; }

        [JsonPropertyName("choices")]
        public ResponseChoiceGPT[] Choices { get; set; }
    }
}
