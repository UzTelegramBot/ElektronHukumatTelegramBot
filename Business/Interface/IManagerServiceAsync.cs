using Business.ModelDTO;
using Domains;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;

namespace Business.Interface
{
    public interface IManagerServiceAsync
    {
        Task<ManagerDTO> CreateAsync(ManagerForCreationDTO managerDto, string currentRegion,string currentRole);
        Task<IReadOnlyList<ManagerDTO>> GetPageListAsync(int page, int pageSize);
        Task<ManagerDTO> GetManagerAsync(Guid Id);
        Task<ManagerDTO> UpdateAsync(ManagerDTO managerDto);
        Task DeleteAsync(Guid Id);
        Task<string> LoginAsync(string login, string password);
        Task<ManagerDTO> LoginExist(string login);
    }
}
