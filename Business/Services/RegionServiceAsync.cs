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
    public class RegionServiceAsync : IRegionServiceAsync
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICheckServiceAsync _checkServiceAsync;

        public RegionServiceAsync(IUnitOfWork unitofWork,
            IMapper mapper,
            ICheckServiceAsync checkServiceAsync 
            )
        {
            _unitOfWork = unitofWork;
            _mapper = mapper;
            _checkServiceAsync = checkServiceAsync;
        }
        public async Task<RegionDTO> CreateAsync(RegionForCreationDTO regionForCreationDTO, Guid currentRegionId)
        {

            var regionOfManager = await _unitOfWork
                .RegionRepository
                .FindByCondition(r=>r.Id == currentRegionId);
            bool checkIndex = _checkServiceAsync
                .CheckRegionBetweenIndex(regionForCreationDTO.RegionIndex, regionOfManager.RegionIndex);
            if (!checkIndex)
                return null;
            
            var IsExist = await _unitOfWork.RegionRepository.FindByCondition(r => r.RegionIndex == regionForCreationDTO.RegionIndex);
            if(IsExist == null)
            {
                var region = await _unitOfWork.RegionRepository.CreateAsync(_mapper.Map<Region>(regionForCreationDTO));
                await _unitOfWork.SaveChangesAsync();
                return _mapper.Map<RegionDTO>(region) ?? null;
            }
            return null;
        }

        public async Task DeleteAsync(Guid Id, Guid currentRegionId)
        {
            bool confirm = await _checkServiceAsync
                      .CheckRegionBetweenRegionsId(currentRegionId, Id);
            if (!confirm)
                return;
            _unitOfWork.RegionRepository.Delete(Id);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<RegionDTO> GetByIdAsync(Guid Id)
        {
            var region = await _unitOfWork.RegionRepository.FindByCondition(r => r.Id == Id);
            return _mapper.Map<RegionDTO>(region);
        }

        public async Task<IReadOnlyList<RegionDTO>> GetPageListAsync(int page, int pageSize)
        {
            var pageList = await _unitOfWork.RegionRepository.GetPageListAsync(page, pageSize);
            return _mapper.Map<IReadOnlyList<RegionDTO>>(pageList);
        }

        public async Task UpdateAsync(RegionDTO regionDTO, Guid currentRegionId)
        {
            bool confirm = await _checkServiceAsync
                    .CheckRegionBetweenRegionsId(currentRegionId, regionDTO.Id);
            if (!confirm)
                return ;

            _unitOfWork.RegionRepository.Update(_mapper.Map<Region>(regionDTO));
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<IReadOnlyList<RegionDTO>> GetRegionByManager(Guid regionId,string regionIdOfManager)
        {
            try
            {
             var region = await GetByIdAsync(regionId);
             var regionOfManager = await GetByIdAsync(Guid.Parse(regionIdOfManager));
             bool checkSimilarIndex = _checkServiceAsync
                    .CheckRegionBetweenIndex(region.RegionIndex, regionOfManager.RegionIndex);

             if(!checkSimilarIndex)
                return null;
             var regions = await _unitOfWork.RegionRepository.GetRegionLayer(region.RegionIndex.ToString());
             return _mapper.Map<IReadOnlyList<RegionDTO>>(regions);
            }
            catch
            {
                return null;
            }
        }
    }
}
