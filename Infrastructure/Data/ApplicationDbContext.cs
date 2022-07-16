using Domains;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Manager> Managers { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<BotTextData> BotTextDatas { get; set; }
        public DbSet<User> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new DefaultRegion());
            builder.ApplyConfiguration(new DefaultOperatorData());
        }
    }
}
