using Business.ModelDTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Interface
{
    public interface IUserServiceAsync
    {

        Task<UserDTO> CreateAsync(UserForCreationDTO userForCreationDTO);
        Task<IReadOnlyList<UserDTO>> GetPageListAsync(int page, int pageSize);
        Task<UserDTO> GetByIdAsync(Guid Id);
        Task UpdateAsync(UserDTO userDTO);
        Task DeleteAsync(Guid Id);
    }
}
