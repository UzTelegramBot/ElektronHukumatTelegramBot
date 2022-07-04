using System.Collections.Generic;
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
    public class SendKeyBoardButtonCommand : ICommand
    {
        private readonly string Text;

        public Language Language { get; }

        private readonly BotTextDataRepositoryAsync BotTextDataRepository;

        public SendKeyBoardButtonCommand(ApplicationDbContext context,
            Language language,
            string text)
        {
            this.Text = text;
            this.Language = language;
            this.BotTextDataRepository = new BotTextDataRepositoryAsync(context);
        }

        public async Task Execute(ITelegramBotClient client, long userId)
        {
            var KeyBoards = await BotTextDataRepository.GetKeyBoard();
            var BotData = await BotTextDataRepository
                .FindByCondition(b => b.TypeData == TypeData.Aware);
            List<string> textKeyBoards = GetTextFromLanguage
                .GetKeyBoardText(Language, KeyBoards);


            await client.SendTextMessageAsync(
               chatId: userId,
               text: Text,
               replyMarkup: ReplyMarkUp.SendKeyBoardButton(textKeyBoards)
               );
        }
    }
}
