using SBA.Business.ExternalServices.ChatGPT.Models;

namespace SBA.Business.ExternalServices.Abstract
{
    public interface IGptResponseGenerator
    {
        Task<string> GenerateResultAsync(List<RequestMessageGPT> messages, RequestConfigGPT requestConfiguration);
    }
}
