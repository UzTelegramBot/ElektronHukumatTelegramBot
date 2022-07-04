using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramBot.Business.Interface;
using TelegramBot.Business.Model;
using TelegramBot.Business.StaticService;
using TelegramBot.Domains;
using TelegramBot.Infrastructure.Data;
using TelegramBot.Infrastructure.Repository;

namespace TelegramBot.Business.Commands
{
    public class ModifiedLanguageCommand : ICommand
    {
        public Chat Chat { get; }
        public int MessageId { get; }
        private string Language { get; }

        private readonly ApplicationDbContext _context;
        private readonly UserRepositoryAsync userRepository;
        private readonly BotTextDataRepositoryAsync botTextDataRepository;

        public ModifiedLanguageCommand(ApplicationDbContext context,
            string language,
            int messageId,
            Chat chat
            )
        {
            this.Chat = chat;
            this.MessageId = messageId; 
            this.Language = language;
            _context = context;
            userRepository = new UserRepositoryAsync(context);
            botTextDataRepository = new BotTextDataRepositoryAsync(context);
        }
        public async Task Execute(ITelegramBotClient client, long userId)
        {
            var user = await userRepository.GetById(userId);
            user.Language = Language switch
            {
                "Uz" => Domains.Language.Uz,
                "Ru" => Domains.Language.Ru,
                "Eng" => Domains.Language.Eng,
                _ => Domains.Language.Uz
            };
            await userRepository.Update(user);
            var botData = await botTextDataRepository
                .FindByCondition(b => b.TypeData == TypeData.Selected);
            var text = GetTextFromLanguage.GetText(user.Language, botData);
            await client.DeleteMessageAsync(
                Chat,
                MessageId
                );
            await new SendKeyBoardButtonCommand(_context, user.Language, text)
                .Execute(client, userId);
            if(user.PhoneNumber == null)
            {
              await new AskContactNumberCommand(_context, user.Language).Execute(client, userId);
            }
        }
    }
}
