using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace Business.Interface
{
    public abstract class SendMessageAbstract
    {
        public abstract Task SendMessage(Message message);
    }
}
