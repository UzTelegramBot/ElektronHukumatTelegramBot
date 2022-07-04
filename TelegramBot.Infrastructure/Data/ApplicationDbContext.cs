using Microsoft.EntityFrameworkCore;
using TelegramBot.Domains;

namespace TelegramBot.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Region> Regions { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<BotTextData> BotTextDatas { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string Connection = "Server=localhost;" +
                                "Port=5432;" +
                                "Database=TelegramBotNotification;" +
                                "Username=postgres;" +
                                "Password=20020623;";

            optionsBuilder.UseNpgsql(Connection);
        }
    }
}
