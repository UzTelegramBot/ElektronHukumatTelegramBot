using Domains;
using System.Threading.Tasks;

namespace Infrastructure.Interface
{
    public interface IManagerRepositoryAsync : IBaseRepositoryAsync<Manager>
    {
        Task<Manager> LoginAsync(string login, string password);
    }
}
