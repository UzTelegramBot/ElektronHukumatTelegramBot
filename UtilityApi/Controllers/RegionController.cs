using Business.Interface;
using Business.ModelDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
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
        public async Task<IActionResult> Create([FromBody] RegionForCreationDTO  regionForCreationDTO)
        {
            if (ModelState.IsValid)
            {
                var region = await _service.CreateAsync(regionForCreationDTO);
                if (region != null)
                    return Ok(region);
            }
            return NotFound();
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody] RegionDTO regionDTO)
        {
            if (ModelState.IsValid)
            {
                await _service.UpdateAsync(regionDTO);
                return Ok();
            }
            return NotFound();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id != Guid.Empty)
            {
                await _service.DeleteAsync(id);
                return Ok();
            }
            return NotFound();
        }
    }
}
