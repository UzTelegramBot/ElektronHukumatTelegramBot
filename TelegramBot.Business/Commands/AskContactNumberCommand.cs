using System.Threading.Tasks;
using Telegram.Bot;
using TelegramBot.Business.Interface;
using TelegramBot.Business.ReplyMarkUps;
using TelegramBot.Business.StaticService;
using TelegramBot.Domains;
using TelegramBot.Infrastructure.Data;
using TelegramBot.Infrastructure.Repository;

namespace TelegramBot.Business.Commands
{
    public class AskContactNumberCommand : ICommand
    {
        private readonly Language Language;

        private readonly BotTextDataRepositoryAsync _botTextDataReposiotory;

        public AskContactNumberCommand(ApplicationDbContext context, Language language)
        {
            this.Language = language;
            _botTextDataReposiotory = new BotTextDataRepositoryAsync(context);
        }
        public async Task Execute(ITelegramBotClient client, long userId)
        {
            var data = await _botTextDataReposiotory
                .FindByCondition(b => b.TypeData == TypeData.Contact);
            
            var text = GetTextFromLanguage.GetText(Language, data);
            await client.SendTextMessageAsync(
                chatId: userId,
                text : text,
                replyMarkup: ReplyMarkUp.SendContact("Contact")
                );
        }
    }
}
