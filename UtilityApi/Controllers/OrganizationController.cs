using Business.Interface;
using Business.ModelDTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;


namespace UtilityApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganizationController : ControllerBase
    {
        private readonly IOrganizationServiceAsync _service;
        public OrganizationController(IOrganizationServiceAsync service)
        {
            _service = service;
        }
        [HttpGet]
        [Authorize(Roles ="Admin,Organizator")]
        public async Task<IActionResult> GetPageList([FromQuery] int page, int pageSize)
        {
            if (page <= 0 || pageSize <= 0)
                return BadRequest();
            ClaimsIdentity identity = (ClaimsIdentity)User.Identity;
            Guid RegionId = Guid.Parse(identity.FindFirst("RegionId").Value);
            var Role = identity.FindFirst(ClaimTypes.Role).Value;
            IReadOnlyList<OrganizationDTO> organizations;
            if (Role == "Admin")
            {
                organizations = await _service.GetPageListByRegionId(page, pageSize, RegionId);
            }
            else
            {
             Guid OrganizationId = Guid.Parse(identity.FindFirst("OrganizationId").Value); 
              organizations = await _service.GetPageListAsync(page, pageSize,OrganizationId);
            }

            if(organizations == null)
                return NotFound();

            return Ok(organizations);
        }

        [HttpGet("{id}")]
        [Authorize(Roles ="Admin,Organizator")]
        public async Task<IActionResult> Get(Guid id)
        {
            if (!id.Equals(Guid.Empty))
            {
                ClaimsIdentity identity = (ClaimsIdentity)User.Identity;
                var managerId = Guid
                    .Parse(identity.FindFirst(ClaimTypes.NameIdentifier).Value);
                var organization = await _service.GetByIdAsync(id,managerId);

                if (organization != null)
                    return Ok(organization);
            }
            return NotFound();
        }

        [HttpPost]
        [Authorize(Roles ="Admin,Organizator")]
        public async Task<IActionResult> Create([FromBody] OrganizationForCreationDTO organizationForCreation)
        {
            if (ModelState.IsValid)
            {
                ClaimsIdentity identity = (ClaimsIdentity)User.Identity;
                var role = identity.FindFirst(ClaimTypes.Role).Value;
                Guid currentRegionId = Guid.Parse(identity.FindFirst("RegionId").Value);
                if(role != "Admin")
                {
                    organizationForCreation.ParentId = Guid
                        .Parse(identity.FindFirst("OrganizationId").Value);
                }
                organizationForCreation.Creaetedby = Guid
                    .Parse(identity.FindFirst(ClaimTypes.NameIdentifier).Value);
                organizationForCreation.CreatedDate = DateTime.Now;
                var organization = await _service.CreateAsync(organizationForCreation, currentRegionId);
                
                if (organization != null)
                    return Ok(organization);
            }
            return NotFound();
        }

        [HttpPut]
        [Authorize(Roles ="Admin,Organizator")]
        public async Task<IActionResult> Update([FromBody] OrganizationDTO organizationDTO)
        {
            if(ModelState.IsValid)
            {
                ClaimsIdentity identity = (ClaimsIdentity)User.Identity;
                var managerId = Guid
                    .Parse(identity.FindFirst(ClaimTypes.NameIdentifier).Value);

                organizationDTO.LastModifiedby = Guid
                    .Parse(identity.FindFirst(ClaimTypes.NameIdentifier).Value);
                organizationDTO.LastModifiedDate = DateTime.Now;
                await _service.UpdateAsync(organizationDTO,managerId);
                 return Ok(organizationDTO);
            }
            return NotFound();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles ="Admin,Organizator")]
        public async Task<IActionResult> Delete(Guid id)
        {
            if(id != Guid.Empty)
            {
                ClaimsIdentity identity = (ClaimsIdentity)User.Identity;
                var managerId = Guid
                    .Parse(identity.FindFirst(ClaimTypes.NameIdentifier).Value);
                await _service.DeleteAsync(id,managerId);
                return Ok();
            }
            return NotFound();
        }
    }
}
