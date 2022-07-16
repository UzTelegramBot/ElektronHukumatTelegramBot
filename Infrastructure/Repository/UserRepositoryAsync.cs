using Domains;
using Infrastructure.Data;
using Infrastructure.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class UserRepositoryAsync : BaseRepositoryAsync<User>, IUserRepositoryAsync
    {
        private readonly DbSet<User> _users;

        public UserRepositoryAsync(ApplicationDbContext context) : base(context)
        {
            _users = context.Users;
        }
        public async Task<IReadOnlyList<User>> GetListById(Guid RegionId, Language language, int page, int pageSize)
        {
            IReadOnlyList<User> users = await _users.AsNoTracking()
                .Where(u => u.RegionId == RegionId &&
                       u.Language == language)
                .Skip((page - 1) * pageSize)
                .Take(pageSize).ToListAsync();
            return users;
        }
        public int GetCount(Guid RegionId)
        {
            return _users.AsNoTracking()
                .Where(u => u.RegionId == RegionId).ToList().Count;
        }
    }
}
