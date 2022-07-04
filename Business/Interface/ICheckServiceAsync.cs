using Domains;
using System;
using System.Threading.Tasks;

namespace Business.Interface
{
    public interface ICheckServiceAsync
    {
        Task<bool> CheckRegionBetweenRegionsId(Guid currentRegionId, Guid regionId);
        bool CheckOrganizationForModifiedManager(Guid? currentManagerOrganizationId, Guid? managerOrganizationId);
        bool CheckRoleForCreationManager(string currentRole, RoleManager managerRole);
        public bool CheckRegionBetweenIndex(long regionIndex, long mainRegionIndex);
    }
}
