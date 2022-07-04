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
    public class ShowLanguagesCommand : ICommand
    {
        private readonly UserRepositoryAsync userRepository;
        private readonly BotTextDataRepositoryAsync botTextData;

        public ShowLanguagesCommand(ApplicationDbContext context)
        {
            userRepository = new UserRepositoryAsync(context);
            botTextData = new BotTextDataRepositoryAsync(context);
        }

        public async Task Execute(ITelegramBotClient client, long userId)
        {
            var user = await userRepository.GetById(userId);

            var botData = await botTextData
                .FindByCondition(b => b.TypeData == TypeData.Language);

            var text = GetTextFromLanguage.GetText(user.Language, botData);

            await client.SendTextMessageAsync(
                chatId: userId,
                text: text,
                replyMarkup: ReplyMarkUp.OptionsOfLanguage()
                );
        }
    }
}
