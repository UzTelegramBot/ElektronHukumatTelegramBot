using Infrastructure.Data;
using Infrastructure.Interface;
using System;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private ManagerRepositoryAsync managerRepositoryAsync;
        private RegionRepositoryAsync regionRepositoryAsync;
        private OrganizationRepositoryAsync organizationRepositoryAsync;
        private UserRepositoryAsync userRepositoryAsync;
        private MessageRepositoryAsync messageRepositoryAsync;
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }
        public IManagerRepositoryAsync ManagerRepository => managerRepositoryAsync ?? new ManagerRepositoryAsync(_context);

        public IRegionRepositoryAsync RegionRepository => regionRepositoryAsync ?? new RegionRepositoryAsync(_context);

        public IOrganizationRepostioryAsync OrganizationRepostiory => organizationRepositoryAsync ?? new OrganizationRepositoryAsync(_context);


        public IUserRepositoryAsync UserRepository => userRepositoryAsync ?? new UserRepositoryAsync(_context);

        public IMessageRepositoryAsync MessageRepository => messageRepositoryAsync ?? new MessageRepositoryAsync(_context);

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
        public void Dispose()   
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
