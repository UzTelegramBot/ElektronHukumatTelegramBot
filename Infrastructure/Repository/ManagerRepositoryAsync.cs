using Domains;
using Infrastructure.Data;
using Infrastructure.Interface;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class ManagerRepositoryAsync : BaseRepositoryAsync<Manager>, IManagerRepositoryAsync
    {
        private readonly DbSet<Manager> _managers;

        public ManagerRepositoryAsync(ApplicationDbContext context) : base(context)
        {
            _managers = context.Set<Manager>();
        }

        public async Task<Manager> LoginAsync(string login, string password) =>
               await FindByCondition(item => item.Login == login && item.Password == password);
            
            
    }
}
