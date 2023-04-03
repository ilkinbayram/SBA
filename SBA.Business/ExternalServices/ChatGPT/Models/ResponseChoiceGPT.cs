using System.Text.Json.Serialization;

namespace SBA.Business.ExternalServices.ChatGPT.Models
{
    public class ResponseChoiceGPT
    {
        [JsonPropertyName("message")]
        public ResponseMessageGPT Message { get; set; }

        [JsonPropertyName("finish_reason")]
        public string FinishReason { get; set; }

        [JsonPropertyName("index")]
        public int Index { get; set; }
    }
}
