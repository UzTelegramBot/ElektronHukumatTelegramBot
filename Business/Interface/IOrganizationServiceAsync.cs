using Business.ModelDTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Interface
{
    public interface IOrganizationServiceAsync
    {
        Task<OrganizationDTO> CreateAsync(OrganizationForCreationDTO organizationForCreationDTO, Guid CurrentRegionId);
        Task<IReadOnlyList<OrganizationDTO>> GetPageListAsync(int page, int pageSize, Guid OrganizationId);
        Task<IReadOnlyList<OrganizationDTO>> GetPageListByRegionId(int page, int pageSize, Guid RegionId);
        Task<OrganizationDTO> GetByIdAsync(Guid Id, Guid ManagerId);
        Task UpdateAsync(OrganizationDTO organizationDTO,Guid ManagerId);
        Task DeleteAsync(Guid Id,Guid ManagerId);
    }
}
