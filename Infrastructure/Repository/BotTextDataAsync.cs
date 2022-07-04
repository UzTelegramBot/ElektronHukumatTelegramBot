using Domains;
using Infrastructure.Data;
using Infrastructure.Interface;

namespace Infrastructure.Repository
{
    public class BotTextDataAsync : BaseRepositoryAsync<BotTextData>, IButtonRepositoryAsync
    {
        public BotTextDataAsync(ApplicationDbContext context) : base(context)
        {
        }
    }
}
