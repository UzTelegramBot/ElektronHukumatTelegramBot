using Business.ModelDTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Interface
{
    public interface IMessageServiceAsync
    {
        Task<MessageDTO> CreateAsync(MessageForCreationDTO messageForCreationDTO);
        Task<MessageDTO> GetByIdAsync(Guid Id);
    }
}
