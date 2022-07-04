using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TelegramBot.Domains;
using TelegramBot.Infrastructure.Data;

namespace TelegramBot.Infrastructure.Repository
{
    public class BotTextDataRepositoryAsync
    {
        private readonly ApplicationDbContext _context;

        public BotTextDataRepositoryAsync(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<BotTextData> FindByCondition(Expression<Func<BotTextData, bool>> expression)
        {
            try
            {
               return await _context.BotTextDatas.AsNoTracking().FirstOrDefaultAsync(expression);

            }
            catch
            {

            }
            return null;
        }
        public async  Task<List<BotTextData>> GetKeyBoard() =>
            await _context.BotTextDatas.Where(b => b.TypeData == TypeData.KeyboardButton).ToListAsync();
    }
}
