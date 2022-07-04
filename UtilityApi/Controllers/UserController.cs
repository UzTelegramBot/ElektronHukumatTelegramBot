using Business.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace UtilityApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserServiceAsync _service;

        public UserController(IUserServiceAsync service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<IActionResult> GetPageListAsync([FromQuery] int page, int pageSize)
        {
            if (page <= 0 || pageSize <= 0)
                return BadRequest();
            var users = await _service.GetPageListAsync(page, pageSize);
            return Ok(users);
        }
    }
}
