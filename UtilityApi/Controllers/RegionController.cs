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
        private readonly ICheckServiceAsync _checkService;
        private readonly IRegionServiceAsync _service;
        
        public RegionController(IRegionServiceAsync service, 
            ICheckServiceAsync checkServiceAsync)
        {
            _checkService = checkServiceAsync;
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

                var regionOfManager =await _service.GetByIdAsync(regionIdOfManager);
                bool checkIndex = _checkService
                    .CheckRegionBetweenIndex(regionForCreationDTO.RegionIndex,regionOfManager.RegionIndex);
                if (!checkIndex)
                    return BadRequest();

                var region = await _service.CreateAsync(regionForCreationDTO);
                if (region != null)
                    return Ok(region);
            }
            return NotFound();
        }
        [HttpPut("{id}")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> Put([FromBody] RegionDTO regionDTO)
        {
            if (ModelState.IsValid)
            {
                 ClaimsIdentity identity = (ClaimsIdentity)User.Identity;
                 Guid regionIdOfManager = Guid.Parse(identity.FindFirst("RegionId").Value);
                 bool confirm = await _checkService
                    .CheckRegionBetweenRegionsId(regionIdOfManager, regionDTO.Id);
                if (!confirm)
                    return BadRequest();

                await _service.UpdateAsync(regionDTO);
                return Ok();
            }
            return NotFound();
        }
        [HttpDelete("{id}")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id != Guid.Empty)
            {
                ClaimsIdentity identity = (ClaimsIdentity)User.Identity;
                Guid regionIdOfManager = Guid.Parse(identity.FindFirst("RegionId").Value);
                bool confirm = await _checkService
                      .CheckRegionBetweenRegionsId(regionIdOfManager, id);
                if (!confirm)
                    return BadRequest();
                await _service.DeleteAsync(id);
                return Ok();
            }
            return BadRequest();
        }
        [HttpGet("RegionsByManager")]
        [Authorize]
        public async Task<IActionResult> GetRegionByManager([FromQuery] Guid region)
        {
              if(region != Guid.Empty)
              {

                var identity = (ClaimsIdentity)User.Identity;
                var regionIdOfManager = identity.FindFirst("RegionId").Value;
                var regions = await _service.GetRegionByManager(region,regionIdOfManager);
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
