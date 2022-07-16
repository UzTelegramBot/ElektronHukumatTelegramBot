using Business.ModelDTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Interface
{
    public interface IRegionServiceAsync 
    {
        Task<RegionDTO> CreateAsync(RegionForCreationDTO regionForCreationDTO, Guid currentRegion);
        Task<IReadOnlyList<RegionDTO>> GetPageListAsync(int page, int pageSize);
        Task<RegionDTO> GetByIdAsync(Guid Id);
        Task UpdateAsync(RegionDTO regionDTO, Guid currentRegionId);
        Task DeleteAsync(Guid Id, Guid currentRegionId);
        Task<IReadOnlyList<RegionDTO>> GetRegionByManager(Guid regionId, string regionIdByManager);
    }
}
