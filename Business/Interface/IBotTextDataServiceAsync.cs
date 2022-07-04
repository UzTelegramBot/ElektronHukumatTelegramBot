using Business.ModelDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interface
{
    public interface IBotTextDataServiceAsync
    {
        Task<BotTextDataDTO> CreateAsync(BotTextDataForCreationDTO buttonForCreaetionDTO);
        Task<IReadOnlyList<BotTextDataDTO>> GetPageListAsync(int page, int pageSize);
        Task<BotTextDataDTO> GetByIdAsync(Guid Id);
        Task UpdateAsync(BotTextDataDTO buttonDto);
        Task DeleteAsync(Guid Id);
    }
}
