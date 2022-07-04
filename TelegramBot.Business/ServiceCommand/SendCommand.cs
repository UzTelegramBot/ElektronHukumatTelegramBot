using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramBot.Business.Interface;

namespace TelegramBot.Business.ServiceCommand
{
    public class SendCommand
    {
        private ICommand _command;
        private readonly ManageCommand _manage;
        private readonly TableCommand _tableCommand;
        private readonly ITelegramBotClient client;

        public SendCommand(ITelegramBotClient client)
        {
            this.client = client;
            _manage = new ManageCommand();
            _tableCommand = new TableCommand();
        }
        public async Task GetTextDataAndSetCommand(Message message)
        {
            _command = await _tableCommand.GetCommandForMessage(message);
            if (_command == null)
                return;
            _manage.SetCommand(_command);
            await _manage.Invoke(client, message.Chat.Id);
        }
        public async Task GetCallBackqueryCommand(CallbackQuery callbackQuery)
        {
            _command = _tableCommand.GetCommandForCallBack(callbackQuery);
            if (_command == null)
                return;
            _manage.SetCommand(_command);
            await _manage.Invoke(client, callbackQuery.From.Id);
        }
    }
}
