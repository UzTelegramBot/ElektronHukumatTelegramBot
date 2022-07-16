using Business.Interface;
using Business.ModelDTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace UtilityApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionController : ControllerBase
    {
        private readonly IRegionServiceAsync _service;
        
        public RegionController(IRegionServiceAsync service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<IActionResult> GetPageList([FromQuery] int page, int pageSize)
        {
            if (page <= 0 || pageSize <= 0) return NotFound();
            var regions = await _service.GetPageListAsync(page, pageSize);

            if (regions == null) return NotFound();

            return Ok(regions);

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            
            if (!id.Equals(Guid.Empty))
            {
                var region = await _service.GetByIdAsync(id);
                if (region != null)
                    return Ok(region);
            }
            return NotFound();
        }
        [HttpPost]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> Create([FromBody] RegionForCreationDTO  regionForCreationDTO)
        {
            if (ModelState.IsValid)
            {
                ClaimsIdentity identity = (ClaimsIdentity)User.Identity;
                Guid regionIdOfManager = Guid.Parse(identity.FindFirst("RegionId").Value);

                var region = await _service.CreateAsync(regionForCreationDTO, regionIdOfManager);
                if (region != null)
                    return Ok(region);
            }
            return NotFound();
        }
        [HttpPut]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> Put([FromBody] RegionDTO regionDTO)
        {
            if (ModelState.IsValid)
            {
                 ClaimsIdentity identity = (ClaimsIdentity)User.Identity;
                 Guid regionIdOfManager = Guid.Parse(identity.FindFirst("RegionId").Value);
                 
                await _service.UpdateAsync(regionDTO,regionIdOfManager);
                return Ok(regionDTO);
            }
            return NotFound();
        }
        [HttpDelete]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id != Guid.Empty)
            {
                ClaimsIdentity identity = (ClaimsIdentity)User.Identity;
                Guid regionIdOfManager = Guid.Parse(identity.FindFirst("RegionId").Value);
                await _service.DeleteAsync(id, regionIdOfManager);
                return Ok();
            }
            return BadRequest();
        }
        [HttpGet("RegionsByManager")]
        [Authorize]
        public async Task<IActionResult> GetRegionByManager([FromQuery] Guid regionId)
        {
              if(regionId != Guid.Empty)
              {
                
                var identity = (ClaimsIdentity)User.Identity;
                var regionIdOfManager = identity.FindFirst("RegionId").Value;
                var regions = await _service.GetRegionByManager(regionId,regionIdOfManager);
                if(regions != null)
                   return Ok(regions);
              }
                return BadRequest();
        }
        [HttpGet("RegionsOfManager")]
        [Authorize]
        public async Task<IActionResult> GetRegionsOfManager()
        {
            try
            {
                var identity = (ClaimsIdentity)User.Identity;
                var regionIdOfManager = identity.FindFirst("RegionId").Value;
                var regions = await _service.GetByIdAsync(Guid.Parse(regionIdOfManager));
                if (regions != null)
                    return Ok(regions);

                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
