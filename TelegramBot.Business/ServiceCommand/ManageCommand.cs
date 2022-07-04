using System.Threading.Tasks;
using Telegram.Bot;
using TelegramBot.Business.Interface;

namespace TelegramBot.Business.ServiceCommand
{
    public class ManageCommand 
    {
        private ICommand _command;

        public void SetCommand(ICommand command) => _command = command;
        public async Task Invoke(ITelegramBotClient client, long userId)
        {
            await _command.Execute(client,userId);
        }
    }
}
