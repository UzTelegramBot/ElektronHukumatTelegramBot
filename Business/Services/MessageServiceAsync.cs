using AutoMapper;
using Business.Interface;
using Business.ModelDTO;
using Domains;
using Infrastructure.Interface;
using System;
using System.Threading.Tasks;

namespace Business.Services
{
    public class MessageServiceAsync : IMessageServiceAsync
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MessageServiceAsync(IUnitOfWork unitOfWork, IMapper mapper) 
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<MessageDTO> CreateAsync(MessageForCreationDTO messageForCreationDTO)
        {
            var message = await _unitOfWork.MessageRepository
                 .CreateAsync(_mapper.Map<Message>(messageForCreationDTO));
            return _mapper.Map<MessageDTO>(message);
        }
        private async Task SendMessageAsync(Message message)
        {
            int countOfRegion = _unitOfWork.RegionRepository.GetCount(message.RegionId);

        }

        public async Task<MessageDTO> GetByIdAsync(Guid Id)
        {
            var message = await _unitOfWork.MessageRepository
                .FindByCondition(m => m.Id == Id);
            return _mapper.Map<MessageDTO>(message);
        }
    }
}
