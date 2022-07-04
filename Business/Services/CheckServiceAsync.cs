using Business.Interface;
using Domains;
using Infrastructure.Interface;
using System;
using System.Threading.Tasks;

namespace Business.Services
{
    public class CheckServiceAsync : ICheckServiceAsync
    {
        private readonly IUnitOfWork _unitOfWork;

        public CheckServiceAsync(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> CheckRegionBetweenRegionsId(Guid currentRegionId, Guid regionId)
        {
            var CurrentRegion = await _unitOfWork.RegionRepository
                .FindByCondition(r => r.Id == currentRegionId);

            var Region = await _unitOfWork.RegionRepository
                .FindByCondition(r => r.Id == regionId);

            bool confirm = Region.RegionIndex.ToString()
                .StartsWith(CurrentRegion.RegionIndex.ToString());

            return confirm;
        }
        public bool CheckOrganizationForModifiedManager(Guid? currentManagerOrganizationId,
            Guid? managerOrganizationId)
        {
            return currentManagerOrganizationId == managerOrganizationId;
        }
        public bool CheckRoleForCreationManager(string currentRole, RoleManager managerRole)
        {
            bool confirmRole = false;
            if (currentRole == RoleManager.Admin.ToString())
            {
                confirmRole = managerRole == RoleManager.Admin || managerRole == RoleManager.Organizator;
            }
            else if (currentRole == RoleManager.Organizator.ToString())
            {
                confirmRole = managerRole == RoleManager.Operator || managerRole == RoleManager.Organizator;
            }
            return confirmRole;
        }
        public bool CheckRegionBetweenIndex(long regionIndex, long mainRegionIndex) =>
            regionIndex.ToString().StartsWith(mainRegionIndex.ToString());  
    }
}
