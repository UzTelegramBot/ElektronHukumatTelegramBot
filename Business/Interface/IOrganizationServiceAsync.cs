using Business.ModelDTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Interface
{
    public interface IOrganizationServiceAsync
    {
        Task<OrganizationDTO> CreateAsync(OrganizationForCreationDTO organizationForCreationDTO);
        Task<IReadOnlyList<OrganizationDTO>> GetPageListAsync(int page, int pageSize);
        Task<OrganizationDTO> GetByIdAsync(Guid Id);
        Task UpdateAsync(OrganizationDTO organizationDTO);
        Task DeleteAsync(Guid Id);
    }
}
