using System.Text.Json.Serialization;

namespace SBA.Business.ExternalServices.ChatGPT.Models
{
    public class RequestGPT
    {
        [JsonPropertyName("model")]
        public string Model { get; set; } = "gpt-3.5-turbo";

        [JsonPropertyName("max_tokens")]
        public int MaxTokens { get; set; } = 300;

        [JsonPropertyName("messages")]
        public RequestMessageGPT[] Messages { get; set; }
    }

    public class ExampleObject
    {
        public int ExampleNumber { get; set; }
        public string ExampleText { get; set; }
        public NestedObject1 NestedFirstModel { get; set; }
    }

    public class NestedObject1
    {
        public int NestedNumber { get; set; }
        public string NestedText { get; set; }

        public NestedObject2 NestedSecondModel { get; set; }
        public List<CollectioModelCustom> ListItems { get; set; }

    }

    public class CollectioModelCustom
    {
        public int Id { get; set; }
        public string Value { get; set; }
    }

    public class NestedObject2
    {
        public int ExtraNumber { get; set; }
        public string ExtraText { get; set; }
    }
}
