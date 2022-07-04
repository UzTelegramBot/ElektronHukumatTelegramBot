using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TelegramBot.Domains;
using TelegramBot.Infrastructure.Data;
using TelegramBot.Infrastructure.Interface;

namespace TelegramBot.Infrastructure.Repository
{
    public class RegionRepositoryAsync : IRegionRepositoryAsync
    {
        private readonly ApplicationDbContext _context;

        public RegionRepositoryAsync(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Region> GetByIdAsync(Guid Id)
        {
            var region = await _context.Regions
                .FirstOrDefaultAsync(r => r.Id == Id);
            return region;
        }

        public async Task<IReadOnlyList<Region>> GetRegionLayerAsync(Guid RegionId)
        {
            try
            {
                var region = await _context.Regions
               .FirstOrDefaultAsync(r => r.Id == RegionId);
                var regionIndex = region.RegionIndex.ToString();
                IReadOnlyList<Region> RegionLayer;
                int next = 0;
                foreach (int type in Enum.GetValues(typeof(RegionType)))
                {
                    if (next == regionIndex.Length)
                    {
                        RegionLayer = await _context.Regions.Where(r => r.RegionIndex.ToString().Length == type &&
                        r.RegionIndex.ToString().StartsWith(regionIndex))
                        .ToListAsync();
                        return RegionLayer;
                    }
                    next = type;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
            
        }
        /*public async Task<Guid> GetParentsRegionId(Guid RegionId)
        {
            try
            {
                var region = await _context.Regions
               .FirstOrDefaultAsync(r => r.Id == RegionId);
                var regionIndex = region.RegionIndex.ToString();
                int previousLayer = (int)RegionType.second;
                if(previousLayer < regionIndex.Length)
                {
                    foreach (int type in Enum.GetValues(typeof(RegionType)))
                    {
                        if (type == regionIndex.Length)
                        {
                            region = await _context.Regions
                                .FirstOrDefaultAsync(r => r.RegionIndex.ToString() == regionIndex.Substring(0, previousLayer));
                            return region.Id;
                        }
                        previousLayer = type;
                    }
                }

            }
            catch (Exception ex)
            {

            }
               return RegionId;
        }*/

    }
}
