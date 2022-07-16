using Domains;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Interface
{
    public interface IOrganizationRepostioryAsync : IBaseRepositoryAsync<Organization>
    {
        Task<IReadOnlyList<Organization>> GetPageListAsync(int page, int pageSize, Guid OrganizationId);
        Task<IReadOnlyList<Organization>> GetPageListAsyncByRegionId(int page, int pageSize, Guid RegionId);
    }
}
