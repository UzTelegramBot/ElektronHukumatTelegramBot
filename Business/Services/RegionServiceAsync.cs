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

        public RegionServiceAsync(IUnitOfWork unitofWork, IMapper mapper)
        {
            _unitOfWork = unitofWork;
            _mapper = mapper;
        }
        public async Task<RegionDTO> CreateAsync(RegionForCreationDTO regionForCreationDTO)
        {
            var IsExist = await _unitOfWork.RegionRepository.FindByCondition(r => r.RegionIndex == regionForCreationDTO.RegionIndex);
            if(IsExist == null)
            {
                var region = await _unitOfWork.RegionRepository.CreateAsync(_mapper.Map<Region>(regionForCreationDTO));
                await _unitOfWork.SaveChangesAsync();
                return _mapper.Map<RegionDTO>(region) ?? null;
            }
            return null;
        }

        public async Task DeleteAsync(Guid Id)
        {
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

        public async Task UpdateAsync(RegionDTO regionDTO)
        {
            _unitOfWork.RegionRepository.Update(_mapper.Map<Region>(regionDTO));
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
