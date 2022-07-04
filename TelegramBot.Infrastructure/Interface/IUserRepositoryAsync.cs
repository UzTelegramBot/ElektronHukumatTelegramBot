using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramBot.Domains;

namespace TelegramBot.Infrastructure.Interface
{
    public interface IUserRepositoryAsync
    {
        Task<User> Create(User user);
        Task Update(User user);
        Task<User> GetById(long id);
    }
}
