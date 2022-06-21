using AutoMapper;
using Business.Interface;
using Business.ModelDTO;
using Domains;
using Infrastructure.Data;
using Infrastructure.Interface;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
    public class ManagerServiceAsync : IManagerServiceAsync
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ManagerServiceAsync(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<ManagerDTO> CreateAsync(ManagerForCreationDTO managerDto, string currentRegion, string currentRole)
        {
            var confirmRegion = await CheckRegionForCreationManager(currentRegion, managerDto.RegionId);
            var confirmRole = CheckRoleForCreationManager(currentRole, managerDto.Role);
            if (confirmRegion && confirmRole)
            {
               var manager = await _unitOfWork.ManagerRepository
                      .CreateAsync(_mapper.Map<Manager>(managerDto));
            
               await _unitOfWork.SaveChangesAsync();
               return _mapper.Map<ManagerDTO>(manager);
            }
            return null;
        }
       
        public async Task<ManagerDTO> GetManagerAsync(Guid Id)
        {
            var manager = await _unitOfWork.ManagerRepository
                .FindByCondition(m => m.Id == Id, new List<string> { "Organization","Region"});
            
            return _mapper.Map<ManagerDTO>(manager);
        }

        public async Task<IReadOnlyList<ManagerDTO>> GetPageListAsync(int page, int pageSize)
        {
            var managers = _mapper.Map<IReadOnlyList<ManagerDTO>>(
                await _unitOfWork.ManagerRepository.GetPageListAsync(page,pageSize));

            return managers;
        }

        public async Task DeleteAsync(Guid Id)
        {
            _unitOfWork.ManagerRepository.Delete(Id);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<ManagerDTO> UpdateAsync(ManagerDTO managerDto)
        {
            var ExistManager = await _unitOfWork.ManagerRepository
                .FindByCondition(m => m.Login == managerDto.Login);
            if(ExistManager is null)
            {
                _unitOfWork.ManagerRepository.Update(_mapper.Map<Manager>(managerDto));
            await _unitOfWork.SaveChangesAsync();
                return managerDto;
            }
            else if(ExistManager.Id == managerDto.Id && ExistManager is not null)
            {
                _unitOfWork.ManagerRepository.Update(_mapper.Map<Manager>(managerDto));
            await _unitOfWork.SaveChangesAsync();
                return managerDto;
            }
            return null;
        }

        public async Task<ManagerDTO> LoginExist(string login) =>
             _mapper.Map<ManagerDTO>(await _unitOfWork.ManagerRepository.FindByCondition(m => m.Login == login));
        
        #region LoginMethod
        public async Task<string> LoginAsync(string login, string password)
        {
            var manager = _mapper.Map<ManagerDTO>(await _unitOfWork.ManagerRepository.LoginAsync(login,password));
            if (manager == null) return null;
            var claims = GenerateClaim(manager);
            return GetToken(claims);
        }
        public string GetToken(List<Claim> claims)
        {
            var jwt = new JwtSecurityToken(
                issuer: AuthOptions.ISSUER,
                audience: AuthOptions.AUDIENCE,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(10),
                signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256)
                );
            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
        public List<Claim> GenerateClaim(ManagerDTO manager)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, manager.Id.ToString()),
                new Claim(ClaimTypes.GivenName, manager.FirstName),
                new Claim(ClaimTypes.Name, manager.LastName),
                new Claim(ClaimTypes.Role, manager.Role.ToString()),
                new Claim(ClaimTypes.Email, manager.Email),
                new Claim("RegionId",manager.RegionId.ToString())
            };
            return claims;
        }
        #endregion
        
        #region CheckParametres
        public async Task<bool> CheckRegionForCreationManager(string currentRegion, Guid regionId)
        {
            var CurrentRegion = await _unitOfWork.RegionRepository
                .FindByCondition(r => r.Id == Guid.Parse(currentRegion));

            var Region = await _unitOfWork.RegionRepository
                .FindByCondition(r => r.Id == regionId);

            bool confirm = Region.RegionIndex.ToString()
                .StartsWith(CurrentRegion.RegionIndex.ToString());

            return confirm;
        }
        public bool CheckRoleForCreationManager(string currentRole, RoleManager managerRole)
        {
            bool confirmRole = false;
            if(currentRole == RoleManager.Admin.ToString())
            {
                confirmRole = managerRole == RoleManager.Admin || managerRole == RoleManager.Organizator;
            }
            else if(currentRole == RoleManager.Organizator.ToString())
            {
                confirmRole = managerRole == RoleManager.Operator;
            }
            return confirmRole;
        }
        #endregion
    }
}
