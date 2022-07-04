using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using TelegramBot.Business.Commands;
using TelegramBot.Business.Interface;
using TelegramBot.Domains;
using TelegramBot.Infrastructure.Data;
using TelegramBot.Infrastructure.Repository;

namespace TelegramBot.Business.ServiceCommand
{
    public class TableCommand
    {
        private readonly ApplicationDbContext _context;

        public TableCommand()
        {
            _context = new ApplicationDbContext();
        }
        public  async Task<ICommand>GetCommandForMessage(Message message)
        {
            var command = message.Type switch
            {
                MessageType.Text =>
                    await ReturnCommandFromText(message),
                MessageType.Contact =>
                    ReturnCommandFromContact(message.Contact.PhoneNumber),
                _ => null
            };
            return command;

        }

        public ICommand GetCommandForCallBack(CallbackQuery callbackQuery)
        {
            var Chat = callbackQuery.Message.Chat;
            var messageId = callbackQuery.Message.MessageId;
            var data = callbackQuery.Data;
            if (data is null)
                return null;
            switch (data)
            {
                case "Uz":
                case  "Ru":
                case "Eng": return new ModifiedLanguageCommand(_context, data,messageId, Chat);
                default:
                    return new ShowListRegionsByCallBackCommand(_context,data,messageId, Chat);
            }
        }
        private async Task<ICommand> ReturnCommandFromText(Message message)
        {
            var userId = message.Chat.Id;
            var text = message.Text;
            if (text == "/start")
            {
                return new StartCommand(_context,
                    message.Chat.FirstName,
                    message.Chat.LastName,
                    message.Chat.Username);
            }
            var Button = await GetRequest(text, userId);
            if(Button != null)
            {

                ICommand command = Button.Uz switch
                {
                    "Tilni o\'zgartirish" => new ShowLanguagesCommand(_context),
                    "Hududni o\'zgartirish" => new ShowListRegionsByTextCommand(_context),
                    "Biz haqimizda" => null,
                    "Biz bilan aloqa" => null,
                    _ => null
                };
            return command;
            }
            return null;
        }
        private ICommand ReturnCommandFromContact(string phoneNumber)
        {

                return new ModifyContactNumberCommand(_context, phoneNumber);
        }

        private async Task<BotTextData> GetRequest(string text, long userId)
        { 
            var userRepository = new UserRepositoryAsync(_context);
            var buttonRepository = new BotTextDataRepositoryAsync(_context);

            var user = await userRepository.FindByCondition(u => u.UserId == userId);
            return user.Language switch
            {
                Language.Uz =>
                  await buttonRepository.FindByCondition(b => b.Uz == text),
                Language.Ru =>
                  await buttonRepository.FindByCondition(b => b.Ru == text),
                Language.Eng =>
                  await  buttonRepository.FindByCondition(b => b.Eng == text),
                _ =>
                  await  buttonRepository.FindByCondition(b => b.Uz == text)
            };
        }
    }
}
