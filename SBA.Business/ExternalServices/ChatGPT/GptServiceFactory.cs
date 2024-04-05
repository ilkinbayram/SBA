using SBA.Business.ExternalServices.Abstract;
using SBA.Business.ExternalServices.ChatGPT.Models;
using SBA.Business.ExternalServices.ChatGPT.Models.Generators;

namespace SBA.Business.ExternalServices.ChatGPT
{
    public static class GptServiceFactory
    {
        public static IGptResponseGenerator CreateInstance(GptEngineType engineType, string apiKey)
        {
            switch (engineType)
            {
                case GptEngineType.Default_GPT_3:
                    return new Gpt3Generator(apiKey);
                case GptEngineType.Default_GPT_4:
                    return new Gpt4Generator(apiKey);
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
