using Domains;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Infrastructure.Interface
{
    public interface IManagerRepositoryAsync : IBaseRepositoryAsync<Manager>
    {
        Task<Manager> LoginAsync(string login, string password);
        Task<IReadOnlyList<Manager>> GetManagersAsync(Expression<Func<Manager, bool>> expression);

    }
}
