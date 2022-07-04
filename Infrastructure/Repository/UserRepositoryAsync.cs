using Domains;
using Infrastructure.Data;
using Infrastructure.Interface;

namespace Infrastructure.Repository
{
    public class UserRepositoryAsync : BaseRepositoryAsync<User>, IUserRepositoryAsync
    {
        public UserRepositoryAsync(ApplicationDbContext context) : base(context)
        {
        }
    }
}
