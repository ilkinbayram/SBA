using Google.Cloud.Translation.V2;
using Microsoft.Extensions.Configuration;
using SBA.Business.ExternalServices.Abstract;

namespace SBA.Business.ExternalServices
{
    public class GoogleTranslationService : ITranslationService
    {
        private readonly TranslationClient _client;
        private readonly IConfiguration _configuration;

        public GoogleTranslationService(IConfiguration configuration)
        {
            _configuration = configuration;
            var apiKey = _configuration.GetValue<string>("GoogleTranslate-SecretKey");
            _client = TranslationClient.CreateFromApiKey(apiKey);
        }

        public GoogleTranslationService(string apiKey)
        {
            _client = TranslationClient.CreateFromApiKey(apiKey);
        }

        public string Translate(string sourceText, string sourceLanguageCode, string targetLanguageCode)
        {
            var response = _client.TranslateText(sourceText, targetLanguageCode, sourceLanguageCode);
            return response.TranslatedText;
        }
    }
}
