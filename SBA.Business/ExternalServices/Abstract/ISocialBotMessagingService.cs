namespace SBA.Business.ExternalServices.Abstract
{
    public interface ISocialBotMessagingService
    {
        void SendMessage(string message);
        void SendRiskerMessage(string message);
        void SendNisbiMessage(string message);
        void SendNisbiMessage(long newChatId, string message);
        void SendMessage(long newChatId, string message);
    }
}
