using OpenAI_API.Chat;
using SBA.Business.ExternalServices.Abstract;

namespace SBA.Business.ExternalServices.ChatGPT.Models.Generators
{
    public class Gpt3Generator : BaseEngineGenerator, IGptResponseGenerator
    {
        public Gpt3Generator(string apiKey) : base(apiKey)
        {
        }

        public async Task<string> GenerateResultAsync(List<RequestMessageGPT> messages, RequestConfigGPT requestConfiguration)
        {
            const string INTERNAL_ERROR_MESSAGE = "Internal Server Error: ChatGPT Service is crashed!";

            requestConfiguration ??= new RequestConfigGPT(500, 0.05f);

            int CustomRoleOrder(ChatMessageRole role)
            {
                if (role.Equals(ChatMessageRole.System))
                    return 1;
                if (role.Equals(ChatMessageRole.User))
                    return 2;
                if (role.Equals(ChatMessageRole.Assistant))
                    return 3;

                throw new InvalidOperationException($"Unknown role: {role}");
            }

            var requestMessages = messages
                .OrderBy(m => CustomRoleOrder(m.Role))
                .Select(x => new ChatMessage { Role = x.Role, Content = x.Content })
                .ToList();

            try
            {
                var completions = await OpenAI.Chat.CreateChatCompletionAsync(
                new ChatRequest
                {
                    MaxTokens = requestConfiguration.MaxTokens,
                    Messages = requestMessages,
                    Temperature = requestConfiguration.Temperature,
                    Model = EngineModelConstants.GPT_3_5_Turbo
                });

                return completions.Choices[0].Message.Content;
            }
            catch (Exception)
            {
                return INTERNAL_ERROR_MESSAGE;
            }
        }
    }
}
