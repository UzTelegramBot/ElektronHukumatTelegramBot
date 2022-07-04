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
    public class MessageController : ControllerBase
    {
        private readonly IMessageServiceAsync _service;
        private readonly ICheckServiceAsync _checkerService;

        public MessageController(IMessageServiceAsync service,
            ICheckServiceAsync checkerService
            )
        {
            _service = service;
            _checkerService = checkerService;
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> SendMessage([FromBody] MessageForCreationDTO messageForCreationDTO)
        {
            if (ModelState.IsValid)
            {
                var identity = (ClaimsIdentity)User.Identity;
                var CurrentRegionId = Guid.Parse(identity.FindFirst("RegionId").Value);
                var checkRegion = await _checkerService.
                    CheckRegionBetweenRegionsId(CurrentRegionId, messageForCreationDTO.RegionId);
                if (checkRegion)
                {
                    var message = await _service.CreateAsync(messageForCreationDTO);
                    return Ok(message);
                }
            }
            return BadRequest();
        }
    }
}
