using AutoMapper;
using Business.Interface;
using Business.ModelDTO;
using Domains;
using Infrastructure.Interface;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Business.Services
{
    public class ManagerServiceAsync : IManagerServiceAsync
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICheckServiceAsync _checkServiceAsync;

        public ManagerServiceAsync(IUnitOfWork unitOfWork,
            IMapper mapper,
            ICheckServiceAsync checkServiceAsync
            )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _checkServiceAsync = checkServiceAsync;
        }
        public async Task<ManagerDTO> CreateAsync(
            ManagerForCreationDTO managerDto,
            string currentRegion,
            string currentRole)
        {
            var organization = await _unitOfWork.OrganizationRepostiory
                .FindByCondition(o => o.Id == managerDto.OrganizationId);
            if (organization == null)
                return null;
            var confirmRegion = await _checkServiceAsync.CheckRegionBetweenRegionsId(Guid.Parse(currentRegion), managerDto.RegionId);
            var confirmOrganizationRegion = await _checkServiceAsync
                .CheckRegionBetweenRegionsId(organization.RegionId, managerDto.RegionId);
            var confirmRole = _checkServiceAsync.CheckRoleForCreationManager(currentRole, managerDto.Role);
            if (confirmRegion && confirmRole && confirmOrganizationRegion)
            {
               var manager = await _unitOfWork.ManagerRepository
                      .CreateAsync(_mapper.Map<Manager>(managerDto));
            
               await _unitOfWork.SaveChangesAsync();
               return _mapper.Map<ManagerDTO>(manager);
            }
            return null;
        }
       
        public async Task<ManagerDTO> GetManagerAsync(Guid Id,string CurrentManagerId)  
        {
            var manager = await _unitOfWork.ManagerRepository
                .FindByCondition(m => m.Id == Id, new List<string> { "Organization, Region"});
            if (manager == null)
                return null;
            var currentManager = await _unitOfWork.ManagerRepository
                .FindByCondition(m => m.Id == Guid.Parse(CurrentManagerId));
           
            var checkRegion = await _checkServiceAsync.CheckRegionBetweenRegionsId(currentManager.RegionId, manager.RegionId);
            var checkOrganization = _checkServiceAsync.CheckOrganizationBetweenOrganizationId(currentManager.OrganizationId, manager.OrganizationId);
            if(currentManager.Role == RoleManager.Admin)
            {
                if (checkRegion)
                    return _mapper.Map<ManagerDTO>(manager);
            }
             if (checkOrganization && checkRegion)
                return _mapper.Map<ManagerDTO>(manager);
            else
                return null;
        }

        public async Task<IReadOnlyList<ManagerDTO>> GetPageListAsync(int page, int pageSize,string ManagerId)
        {
            var Id = Guid.Parse(ManagerId);
            var manager = await _unitOfWork.ManagerRepository
                .FindByCondition(m => m.Id == Id, new List<string> { "Region" });
            if (manager == null)
                return null;
            Expression<Func<Manager, bool>> expression;
            if (manager.Role == RoleManager.Admin)
            {
                expression = m => m.Region.RegionIndex
                .ToString()
                .StartsWith(manager.Region.RegionIndex.ToString());
            }
            else
            {
                expression = m => m.OrganizationId == manager.OrganizationId
            && m.Region.RegionIndex.ToString()
            .StartsWith(manager.Region.RegionIndex.ToString());
            }
            var managers = await _unitOfWork.ManagerRepository.GetManagersAsync(expression);
            return _mapper.Map<IReadOnlyList<ManagerDTO>>(managers);
        }

        public async Task DeleteAsync(Guid Id, string currentManagerId)
        {

            var manager = await _unitOfWork.ManagerRepository
                .FindByCondition(m => m.Id == Id);
            if (manager == null)
                return;
            var currentManager = await _unitOfWork.ManagerRepository
                .FindByCondition(m => m.Id == Guid.Parse(currentManagerId));
            var checkRegion = await _checkServiceAsync
                .CheckRegionBetweenRegionsId(currentManager.RegionId, manager.RegionId);
            var checkOrganization = _checkServiceAsync
                .CheckOrganizationBetweenOrganizationId(currentManager.OrganizationId, manager.OrganizationId);
            if (currentManager.Role == RoleManager.Admin)
            {
                if (checkRegion)
                    _unitOfWork.ManagerRepository.Delete(Id);
            }
            else if (checkOrganization && checkRegion)
                _unitOfWork.ManagerRepository.Delete(Id);

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<ManagerDTO> UpdateAsync(ManagerDTO managerDto,string currentManagerId)
        {
            var ExistManager = await _unitOfWork.ManagerRepository
                .FindByCondition(m => m.Login == managerDto.Login);
            if (ExistManager is null)
            {
                _unitOfWork.ManagerRepository.Update(_mapper.Map<Manager>(managerDto));
            }
            else if (ExistManager.Id == managerDto.Id && ExistManager is not null)
            {
                _unitOfWork.ManagerRepository.Update(_mapper.Map<Manager>(managerDto));
            }
            else
                return null;
            var currentManager = await _unitOfWork.ManagerRepository
                .FindByCondition(m => m.Id == Guid.Parse(currentManagerId));

            var checkRegion = await _checkServiceAsync.CheckRegionBetweenRegionsId(currentManager.RegionId, managerDto.RegionId);
            var checkOrganization = _checkServiceAsync.CheckOrganizationBetweenOrganizationId(currentManager.OrganizationId, managerDto.OrganizationId);
            var checkRole = _checkServiceAsync.CheckRoleForCreationManager(currentManager.Role.ToString(), managerDto.Role);

            if (currentManager.Role == RoleManager.Admin)
            {
                if (checkRegion)
                {
                    await _unitOfWork.SaveChangesAsync();
                    return managerDto;
                }
            }
            if (checkOrganization && checkRegion && checkRole)
            {
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
                expires: DateTime.UtcNow.AddDays(15),
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
                new Claim("RegionId",manager.RegionId.ToString()),
                new Claim("OrganizationId", manager.OrganizationId.ToString())
            };
            return claims;
        }
        #endregion
      
    }
}
