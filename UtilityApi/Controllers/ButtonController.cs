using Business.Interface;
using Business.ModelDTO;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace UtilityApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ButtonController : ControllerBase
    {
        private readonly IBotTextDataServiceAsync _service;

        public ButtonController(IBotTextDataServiceAsync service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<IActionResult> GetPageListAsync([FromQuery] int page, int pageSize)
        {
            if(page >= 0 || pageSize >= 0)
            {
                var buttons = await _service.GetPageListAsync(page, pageSize);
                return Ok(buttons);
            }
            return BadRequest();
        }
        [HttpPost("Button")]
        public async Task<IActionResult> CreateAsync([FromBody] BotTextDataForCreationDTO buttonForCreationDTO)
        {
            if (ModelState.IsValid)
            {
                var button = await _service.CreateAsync(buttonForCreationDTO);
                return Ok(button);
            }
            return BadRequest();
        }
    }
}
