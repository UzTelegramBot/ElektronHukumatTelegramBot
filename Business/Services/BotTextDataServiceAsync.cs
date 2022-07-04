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
    public class BotTextDataServiceAsync : IBotTextDataServiceAsync
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BotTextDataServiceAsync(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<BotTextDataDTO> CreateAsync(BotTextDataForCreationDTO buttonForCreaetionDTO)
        {
            var button = await _unitOfWork.ButtonRepository.CreateAsync(_mapper.Map<Domains.BotTextData>(buttonForCreaetionDTO));
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<BotTextDataDTO>(button);
        }

        public Task DeleteAsync(Guid Id)
        {
            throw new NotImplementedException();
        }

        public Task<BotTextDataDTO> GetByIdAsync(Guid Id)
        {
            throw new NotImplementedException();
        }

        public async Task<IReadOnlyList<BotTextDataDTO>> GetPageListAsync(int page, int pageSize)
        {
            var buttons = _mapper.Map<IReadOnlyList<BotTextDataDTO>>(await _unitOfWork.ButtonRepository.GetPageListAsync(page, pageSize));
            return buttons;
        }

        public Task UpdateAsync(BotTextDataDTO buttonDto)
        {
            throw new NotImplementedException();
        }
    }
}
