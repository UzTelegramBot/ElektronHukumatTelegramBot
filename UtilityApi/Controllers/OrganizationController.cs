using Business.Interface;
using Business.ModelDTO;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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
        public async Task<IActionResult> GetPageList([FromQuery] int page, int pageSize)
        {
            if (page <= 0 || pageSize <= 0) return NotFound();

            var organizations = await _service.GetPageListAsync(page, pageSize);

            if(organizations == null) return NotFound();

            return Ok(organizations);
        }

        [HttpGet("{id}")]
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
        public async Task<IActionResult> Create([FromBody] OrganizationForCreationDTO organizationForCreation)
        {
            if (ModelState.IsValid)
            {
               var organization = await _service.CreateAsync(organizationForCreation);
                if (organization != null)
                    return Ok(organization);
            }
            return NotFound();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody] OrganizationDTO organizationDTO)
        {
            if(ModelState.IsValid)
            {
                 await _service.UpdateAsync(organizationDTO);
                 return Ok();
            }
            return NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            if(id != Guid.Empty)
            {
                await _service.DeleteAsync(id);
                return Ok();
            }
            return NotFound();
        }
    }
}
