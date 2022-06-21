using AutoMapper;
using Business.Interface;
using Business.ModelDTO;
using Domains;
using Infrastructure.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Services
{
    public class OrganizationServiceAsync : IOrganizationServiceAsync
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public OrganizationServiceAsync(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<OrganizationDTO> CreateAsync(OrganizationForCreationDTO organizationForCreationDTO)
        {
            var organization = await  _unitOfWork.OrganizationRepostiory
                .CreateAsync(_mapper.Map<Organization>(organizationForCreationDTO));
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<OrganizationDTO>(organization);
        }

        public async Task DeleteAsync(Guid Id)
        {
            _unitOfWork.OrganizationRepostiory.Delete(Id);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<OrganizationDTO> GetByIdAsync(Guid Id)
        {
            var organization = await _unitOfWork.OrganizationRepostiory.FindByCondition(o=>o.Id == Id, new List<string> { "Region"});
            return _mapper.Map<OrganizationDTO>(organization);
        }

        public async Task<IReadOnlyList<OrganizationDTO>> GetPageListAsync(int page, int pageSize)
        {

            var pageList = await _unitOfWork.OrganizationRepostiory.GetPageListAsync(page, pageSize);
            return _mapper.Map<IReadOnlyList<OrganizationDTO>>(pageList);
        }

        public async Task UpdateAsync(OrganizationDTO organizationDTO)
        {
            _unitOfWork.OrganizationRepostiory.Update(_mapper.Map<Organization>(organizationDTO));
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
