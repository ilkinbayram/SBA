namespace SBA.Business.ExternalServices.Abstract
{
    public interface ITranslationService
    {
        string Translate(string sourceText, string sourceLanguageCode, string targetLanguageCode);
    }
}
