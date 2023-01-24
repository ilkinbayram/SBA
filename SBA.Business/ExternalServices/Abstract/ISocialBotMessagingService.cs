using System.Threading.Tasks;

namespace SBA.Business.ExternalServices.Abstract
{
    public interface ISocialBotMessagingService
    {
        void SendMessage(string message);
        void SendMessage(string newBotToken, long newChatId, string message);
    }
}
