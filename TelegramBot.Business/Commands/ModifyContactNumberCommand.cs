using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBot.Business.Interface;
using TelegramBot.Business.StaticService;
using TelegramBot.Domains;
using TelegramBot.Infrastructure.Data;
using TelegramBot.Infrastructure.Repository;

namespace TelegramBot.Business.Commands
{
    public class ModifyContactNumberCommand : ICommand
    {

        private readonly UserRepositoryAsync _userRepository;
        private readonly ApplicationDbContext _context;
        private readonly string contactNumber;
        private readonly BotTextDataRepositoryAsync botTextDataRepository;

        public ModifyContactNumberCommand(
            ApplicationDbContext context,
            string contactNumber)
        {
            _userRepository = new UserRepositoryAsync(context);
            _context = context;
            this.contactNumber = contactNumber;
            botTextDataRepository = new BotTextDataRepositoryAsync(context);
        }
        public async Task Execute(ITelegramBotClient client, long userId)
        {
            var user = await _userRepository.GetById(userId);
            if (user.PhoneNumber != null)
                return;
            user.PhoneNumber = contactNumber;

            await _userRepository.Update(user);

            var data = await botTextDataRepository
                .FindByCondition(b => b.TypeData == TypeData.Regisrter);
            var text = GetTextFromLanguage.GetText(user.Language, data);
            
            await new SendKeyBoardButtonCommand(_context, user.Language, text)
                .Execute(client, userId);
           
            await new ShowListRegionsByTextCommand(_context)
                .Execute(client, userId);
        }
    }
}
