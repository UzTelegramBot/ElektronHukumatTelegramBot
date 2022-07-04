using System.Threading.Tasks;
using Telegram.Bot;

namespace TelegramBot.Business.Interface
{
    public interface ICommand
    {
        Task Execute(ITelegramBotClient client, long userId);
    }
}
