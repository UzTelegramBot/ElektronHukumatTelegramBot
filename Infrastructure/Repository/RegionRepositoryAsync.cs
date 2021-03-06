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
    public class RegionRepositoryAsync : BaseRepositoryAsync<Region>, IRegionRepositoryAsync
    {
        private readonly DbSet<Region> _regions;

        public RegionRepositoryAsync(ApplicationDbContext context) : base(context)
        {
            _regions = context.Regions;
        }
        public async Task<IReadOnlyList<Region>> GetRegionLayer(string regionIndex)
        {
            IReadOnlyList<Region> Regions = null;
            int next = 0;
            foreach(int type in Enum.GetValues(typeof(RegionType)))
            {
                if(next == regionIndex.Length)
                {
                   Regions = await _regions.Where(r =>r.RegionIndex.ToString().Length == type && 
                        r.RegionIndex.ToString().StartsWith(regionIndex))
                        .ToListAsync();
                }
                next = type;
            }
            return Regions;
        }

        public async Task<IReadOnlyList<Region>> GetListById(Guid RegionId, int page, int pageSize)
        {
            var region = await FindByCondition(r => r.Id == RegionId);

            IReadOnlyList<Region> regions = await _regions.AsNoTracking()
                .Where(r => r.RegionIndex.ToString()
                .StartsWith(region.RegionIndex.ToString()))
                .Skip((page - 1) * pageSize)
                .Take(pageSize).ToListAsync();
            return regions;
        }
        public async Task<int> GetCount(Guid RegionId)
        {
            var region = await FindByCondition(r => r.Id == RegionId);

            return  _regions.AsNoTracking()
                .Where(r => r.RegionIndex.ToString()
                .StartsWith(region.RegionIndex.ToString()))
                .ToList().Count;
        }
    }
}
