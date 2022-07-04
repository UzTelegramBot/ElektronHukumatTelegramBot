using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TelegramBot.Domains;

namespace TelegramBot.Infrastructure.Interface
{
    public interface IRegionRepositoryAsync
    {
        Task<Region> GetByIdAsync(Guid Id);
        Task<IReadOnlyList<Region>> GetRegionLayerAsync(Guid RegionId);
    }
}
