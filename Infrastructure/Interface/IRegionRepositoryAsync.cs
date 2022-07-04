using Domains;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Interface
{
    public interface IRegionRepositoryAsync : IBaseRepositoryAsync<Region>
    {
        Task<IReadOnlyList<Region>> GetRegionLayer(string regionIndex);
        int GetCount(Guid RegionId);
        Task<IReadOnlyList<Region>> GetListById(Guid RegionId, int page, int pageSize);
    }
}
