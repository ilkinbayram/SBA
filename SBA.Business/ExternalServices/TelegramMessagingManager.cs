using SBA.Business.ExternalServices.Abstract;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace SBA.Business.ExternalServices
{
    public class TelegramMessagingManager : ISocialBotMessagingService
    {
        private TelegramBotClient telClient = new TelegramBotClient("5036149843:AAFe3JwwBSY2UX2Nrey1RuzH83Fn4E7PIGk", new HttpClient());

        private TelegramBotClient telClientRisker = new TelegramBotClient("5817892439:AAGYVsGYlfjzy39nfDh5pp_N6ah0tT829eo", new HttpClient());

        private List<ChatId> chatIds = new List<ChatId>()
        {
            new ChatId(1050368957)
        };

        public void SendMessage(string message)
        {
            foreach (var chatId in chatIds)
            {
                var result = telClient.SendTextMessageAsync(chatId, message).Result;
            }
        }

        public void SendRiskerMessage(string message)
        {
            foreach (var chatId in chatIds)
            {
                var result = telClientRisker.SendTextMessageAsync(chatId, message).Result;
            }
        }

        public void SendMessage(long newChatId, string message)
        {
            ChatId specialChatId = new ChatId(newChatId);

            var result = telClient.SendTextMessageAsync(specialChatId, message).Result;
        }
    }
}
