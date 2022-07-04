using Microsoft.EntityFrameworkCore;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TelegramBot.Domains;
using TelegramBot.Infrastructure.Data;
using TelegramBot.Infrastructure.Interface;

namespace TelegramBot.Infrastructure.Repository
{
    public class UserRepositoryAsync : IUserRepositoryAsync
    {
        private readonly ApplicationDbContext _context;

        public UserRepositoryAsync(ApplicationDbContext context)
        {
            _context = context;
        }
        public  async Task<User> Create(User user)
        {
            try
            {
                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();
                return user;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return null;
        }

        public async Task<User> GetById(long id)
        {
            try
            {
                var user = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.UserId == id);
            return user;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }
        public async Task Update(User user)
        {
            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        public async Task<User> FindByCondition(Expression<Func<User, bool>> expression) =>
             await _context.Users.AsNoTracking().FirstOrDefaultAsync(expression);
    }
}
