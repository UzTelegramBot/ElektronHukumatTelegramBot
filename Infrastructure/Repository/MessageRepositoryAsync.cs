using Domains;
using Infrastructure.Data;
using Infrastructure.Interface;

namespace Infrastructure.Repository
{
    public class MessageRepositoryAsync : BaseRepositoryAsync<Message>, IMessageRepositoryAsync
    {
        public MessageRepositoryAsync(ApplicationDbContext context) : base(context)
        {
        }
    }
}
