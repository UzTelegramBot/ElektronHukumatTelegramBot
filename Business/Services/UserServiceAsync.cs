using AutoMapper;
using Business.Interface;
using Business.ModelDTO;
using Infrastructure.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Services
{
    public class UserServiceAsync : IUserServiceAsync
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public UserServiceAsync(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public Task<UserDTO> CreateAsync(UserForCreationDTO userForCreationDTO)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Guid Id)
        {
            throw new NotImplementedException();
        }

        public Task<UserDTO> GetByIdAsync(Guid Id)
        {
            throw new NotImplementedException();
        }

        public async Task<IReadOnlyList<UserDTO>> GetPageListAsync(int page, int pageSize)
        {
            var users = await _unitOfWork.UserRepository.GetPageListAsync(page, pageSize);
            return _mapper.Map<IReadOnlyList<UserDTO>>(users);
        }

        public Task UpdateAsync(UserDTO userDTO)
        {
            throw new NotImplementedException();
        }
    }
}
