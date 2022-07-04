using Business.Interface;
using Business.ModelDTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UtilityApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganizationController : ControllerBase
    {
        private readonly IOrganizationServiceAsync _service;
        private readonly ICheckServiceAsync _checkService;
        public OrganizationController(IOrganizationServiceAsync service,
            ICheckServiceAsync checkServiceAsync)
        {
            _checkService = checkServiceAsync;
            _service = service;
        }
        [HttpGet]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> GetPageList([FromQuery] int page, int pageSize)
        {
            if (page <= 0 || pageSize <= 0)
                return BadRequest();

            var organizations = await _service.GetPageListAsync(page, pageSize);

            if(organizations == null)
                return NotFound();

            return Ok(organizations);
        }

        [HttpGet("{id}")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> Get(Guid id)
        {
            if (!id.Equals(Guid.Empty))
            {
                var organization = await _service.GetByIdAsync(id);
                if(organization != null)
                    return Ok(organization);
            }
            return NotFound();
        }

        [HttpPost]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> Create([FromBody] OrganizationForCreationDTO organizationForCreation)
        {
            if (ModelState.IsValid)
            {
                ClaimsIdentity identity = (ClaimsIdentity)User.Identity;
                Guid regionIdOfManager = Guid.Parse(identity.FindFirst("RegionId").Value);
                bool confirmToCreate = await _checkService
                    .CheckRegionBetweenRegionsId(regionIdOfManager, organizationForCreation.RegionId);
                if (!confirmToCreate)
                    return BadRequest();

                var organization = await _service.CreateAsync(organizationForCreation);
                if (organization != null)
                    return Ok(organization);
            }
            return NotFound();
        }

        [HttpPut("{id}")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> Put([FromBody] OrganizationDTO organizationDTO)
        {
            if(ModelState.IsValid)
            {
                ClaimsIdentity identity = (ClaimsIdentity)User.Identity;
                Guid regionIdOfManager = Guid.Parse(identity.FindFirst("RegionId").Value);
                bool confirmUpdate = await _checkService
                   .CheckRegionBetweenRegionsId(regionIdOfManager, organizationDTO.RegionId);
                if (!confirmUpdate)
                    return BadRequest();
                await _service.UpdateAsync(organizationDTO);
                 return Ok();
            }
            return NotFound();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> Delete(Guid id)
        {
            if(id != Guid.Empty)
            {
                ClaimsIdentity identity = (ClaimsIdentity)User.Identity;
                Guid regionIdOfManager = Guid.Parse(identity.FindFirst("RegionId").Value);
                bool confirmUpdate = await _checkService
                   .CheckRegionBetweenRegionsId(regionIdOfManager,id);
                if (!confirmUpdate)
                    return BadRequest();
                await _service.DeleteAsync(id);
                return Ok();
            }
            return NotFound();
        }
    }
}
