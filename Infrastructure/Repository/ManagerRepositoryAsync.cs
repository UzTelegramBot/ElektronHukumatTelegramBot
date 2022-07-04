using Domains;
using Infrastructure.Data;
using Infrastructure.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

        public async Task<IReadOnlyList<Manager>> GetManagersAsync(Expression<Func<Manager, bool>> expression)
        {
            var managers = _managers.AsNoTracking();
            managers = managers.Include("Region");
            return await managers.Where(expression).ToListAsync();
        }

        public async Task<Manager> LoginAsync(string login, string password) =>
               await FindByCondition(item => item.Login == login && item.Password == password);
            
            
    }
}
