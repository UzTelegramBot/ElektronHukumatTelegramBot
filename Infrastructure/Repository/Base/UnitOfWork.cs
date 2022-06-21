using Infrastructure.Data;
using Infrastructure.Interface;
using System;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        private ManagerRepositoryAsync _managerRepositoryAsync;
        private RegionRepositoryAsync _regionRepositoryAsync;
        private OrganizationRepositoryAsync _organizationRepositoryAsync;
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }
        public IManagerRepositoryAsync ManagerRepository => _managerRepositoryAsync ?? new ManagerRepositoryAsync(_context);

        public IRegionRepositoryAsync RegionRepository => _regionRepositoryAsync ?? new RegionRepositoryAsync(_context);

        public IOrganizationRepostioryAsync OrganizationRepostiory => _organizationRepositoryAsync ?? new OrganizationRepositoryAsync(_context);    


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
