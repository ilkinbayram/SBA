using SBA.Business.ExternalServices.Abstract;
using System.Collections.Generic;
using System.Net.Http;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace SBA.Business.ExternalServices
{
    public class TelegramMessagingManager : ISocialBotMessagingService
    {
        private TelegramBotClient telClient = new TelegramBotClient("5036149843:AAFe3JwwBSY2UX2Nrey1RuzH83Fn4E7PIGk", new HttpClient());

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

        public void SendMessage(string newBotToken, long newChatId, string message)
        {
            TelegramBotClient specialTelegramClient = new TelegramBotClient(newBotToken, new HttpClient());
            ChatId specialChatId = new ChatId(newChatId);

            var result = specialTelegramClient.SendTextMessageAsync(specialChatId, message).Result;
        }
    }
}
