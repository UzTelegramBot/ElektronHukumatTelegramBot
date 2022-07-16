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
    public class OrganizationRepositoryAsync : BaseRepositoryAsync<Organization>, IOrganizationRepostioryAsync
    {
        private DbSet<Organization> _organizations;
        private ApplicationDbContext _context;

        public OrganizationRepositoryAsync(ApplicationDbContext context) : base(context)
        {
            _context = context;
            _organizations = context.Organizations;
        }
        public  async Task<IReadOnlyList<Organization>> GetPageListAsync(int page, int pageSize, Guid OrganizationId)
        {
            IReadOnlyList<Organization> organizations = await _organizations
                .AsNoTracking()
                .Where(o => o.ParentId == OrganizationId)
                .Skip((page-1)* pageSize).Take(pageSize)
                .ToListAsync()
                ;
            return organizations;
        }
        public async Task<IReadOnlyList<Organization>> GetPageListAsyncByRegionId(int page, int pageSize, Guid RegionId)
        {
            var region = _context.Regions.FirstOrDefault(r => r.Id == RegionId);
            IReadOnlyList<Organization> organizations = await _organizations
                .AsNoTracking()
                .Include("Region")
                .Where(o => o.Region.RegionIndex.ToString()
                .StartsWith(region.RegionIndex.ToString()))
                .Skip((page - 1) * pageSize).Take(pageSize)
                .ToListAsync();
            return organizations;
        }
        public override void Delete(Guid Id)
        {
            var organization = _organizations
                .Include(o => o.Managers).FirstOrDefault(o => o.Id == Id);
            foreach(var manager in organization.Managers)
            {
                _context.Managers.Remove(manager);
            }
            _organizations.Remove(organization);
        }
        public override void Update(Organization entity)
        {
            _organizations.Update(entity);
        }
    }
}
