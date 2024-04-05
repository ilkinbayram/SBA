using OpenAI_API;

namespace SBA.Business.ExternalServices.ChatGPT.Models.Generators
{
    public class BaseEngineGenerator
    {
        protected readonly OpenAIAPI OpenAI;

        public BaseEngineGenerator(string apiKey)
        {
            OpenAI = new OpenAIAPI(apiKey);
        }
    }
}
