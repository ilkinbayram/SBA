using SBA.Business.ExternalServices.ChatGPT.Models;

namespace SBA.Business.ExternalServices.ChatGPT
{
    public class ChatGPTService
    {
        private readonly string _apiKey;

        public ChatGPTService(string apiKey)
        {
            _apiKey = apiKey;
        }

        public async Task<string> CallOpenAIAsync(List<RequestMessageGPT> requestMessages, RequestConfigGPT requestConfiguration, GptEngineType engineType)
        {
            var generatorEngine = GptServiceFactory.CreateInstance(engineType, _apiKey);
            string result = await generatorEngine.GenerateResultAsync(requestMessages, requestConfiguration);

            return result;
        }
    }
}
