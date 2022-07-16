using AutoMapper;
using Business.Interface;
using Business.ModelDTO;
using Domains;
using Infrastructure.Interface;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Services
{
    public class OrganizationServiceAsync : IOrganizationServiceAsync
    {
        private readonly ICheckServiceAsync _checker;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public OrganizationServiceAsync(
            IUnitOfWork unitOfWork, 
            IMapper mapper,
            ICheckServiceAsync checker)
        {
            _checker = checker;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<OrganizationDTO> CreateAsync(
            OrganizationForCreationDTO organizationForCreationDTO,
            Guid CurrenRegionId)
        {

            var confirmRegion = await _checker
                .CheckRegionBetweenRegionsId(CurrenRegionId, organizationForCreationDTO.RegionId);
            if (!confirmRegion)
                return null;
            var organization = await  _unitOfWork.OrganizationRepostiory
                .CreateAsync(_mapper.Map<Organization>(organizationForCreationDTO));
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<OrganizationDTO>(organization);
        }

        public async Task DeleteAsync(Guid Id,Guid ManagerId)
        {
            var organization = await _unitOfWork.OrganizationRepostiory
                .FindByCondition(o => o.Id == Id);
            var manager = await _unitOfWork
               .ManagerRepository.FindByCondition(m => m.Id == ManagerId);
            if (organization == null)
                return;
            bool confirmOrganization = _checker
                    .CheckOrganizationBetweenOrganizationId(manager.OrganizationId, organization.ParentId);
            bool confirRegion = await _checker
               .CheckRegionBetweenRegionsId(manager.RegionId, organization.RegionId);
            _unitOfWork.OrganizationRepostiory.Delete(Id);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<OrganizationDTO> GetByIdAsync(Guid Id,Guid ManagerId)
        {
            var manager = await _unitOfWork
               .ManagerRepository.FindByCondition(m => m.Id == ManagerId);
            var organization = await _unitOfWork
                .OrganizationRepostiory
                .FindByCondition(o=>o.Id == Id, new List<string> { "Region"});

            bool confirmOrganization = _checker
                .CheckOrganizationBetweenOrganizationId(manager.OrganizationId, organization.ParentId);
            bool confirmRegion = await _checker
               .CheckRegionBetweenRegionsId(manager.RegionId, organization.RegionId);
            if(manager.Role == RoleManager.Admin && confirmRegion)
            {
            return _mapper.Map<OrganizationDTO>(organization);
            }
            if (!confirmRegion && !confirmOrganization)
                return null;

            return _mapper.Map<OrganizationDTO>(organization);
        }

        public async Task<IReadOnlyList<OrganizationDTO>> GetPageListAsync(
            int page, int pageSize,
            Guid OrganizationId)
        {

            var pageList = await _unitOfWork.OrganizationRepostiory.GetPageListAsync(page, pageSize, OrganizationId);
            return _mapper.Map<IReadOnlyList<OrganizationDTO>>(pageList);
        }
        public async Task<IReadOnlyList<OrganizationDTO>> GetPageListByRegionId(int page, int pageSize,
            Guid RegionId)
        {
            var pageList = await _unitOfWork.OrganizationRepostiory.GetPageListAsyncByRegionId(page, pageSize, RegionId);
            return _mapper.Map<IReadOnlyList<OrganizationDTO>>(pageList);
        }

        public async Task UpdateAsync(OrganizationDTO organizationDTO,Guid ManagerId)
        {
            var manager = await _unitOfWork
                .ManagerRepository.FindByCondition(m => m.Id == ManagerId);
            if (manager == null)
                return;
            bool confirmRegion = await _checker
                   .CheckRegionBetweenRegionsId(manager.RegionId, organizationDTO.RegionId);
            bool confirmOrganization =  _checker
                   .CheckOrganizationBetweenOrganizationId(manager.OrganizationId,organizationDTO.Id);
            if (manager.Role == RoleManager.Admin && confirmRegion)
            {
                _unitOfWork.OrganizationRepostiory.Update(_mapper.Map<Organization>(organizationDTO));
                await _unitOfWork.SaveChangesAsync();
            }
            else if (!confirmRegion && !confirmOrganization)
                return;
            else
            {
                _unitOfWork.OrganizationRepostiory.Update(_mapper.Map<Organization>(organizationDTO));
                await _unitOfWork.SaveChangesAsync();
            }

        }
    }
}
