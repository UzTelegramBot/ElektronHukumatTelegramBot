using AutoMapper;
using Business.Interface;
using Business.ModelDTO;
using Infrastructure.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UtilityApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManagerController : ControllerBase
    {
        private readonly IManagerServiceAsync _service;

        public ManagerController(IManagerServiceAsync service)
        {
            _service = service;
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Organizator")]
        public async Task<IActionResult> GetPageList([FromQuery] int page, int pageSize)
        {
            if (page <= 0 || pageSize <= 0)
                return NotFound();
            var identity = (ClaimsIdentity)User.Identity;
            var ManagerId = identity.FindFirst(ClaimTypes.NameIdentifier).Value;

            var managers = await _service.GetPageListAsync(page, pageSize,ManagerId);

            if (managers == null)
                return NotFound();

            return Ok(managers);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Organizator")]
        public async Task<IActionResult> Get(Guid id)
        {
            if (!id.Equals(Guid.Empty))
            {
                var identity = (ClaimsIdentity)User.Identity;
                var CurrentManagerId = identity.FindFirst(ClaimTypes.NameIdentifier).Value;
                var manager = await _service.GetManagerAsync(id,CurrentManagerId);
                if (manager != null)
                    return Ok(manager);
            }
            return BadRequest();
        }

        [HttpPut]
        [Authorize(Roles = "Admin,Organizator")]
        public async Task<IActionResult> Update([FromBody] ManagerDTO managerDTO)
        {
            if (ModelState.IsValid)
            {
                var identity = (ClaimsIdentity)User.Identity;
                var CurrentManagerId = identity.FindFirst(ClaimTypes.NameIdentifier).Value;
                managerDTO.LastModifiedby = Guid
                        .Parse(identity.FindFirst(ClaimTypes.NameIdentifier).Value);
                managerDTO.LastModifiedDate = DateTime.Now;
                var manager =  await _service.UpdateAsync(managerDTO, CurrentManagerId);
                if (manager != null)
                    return Ok(manager);
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,Organizator")]

        public async Task<IActionResult> Delete(Guid id)
        {
            if (id != Guid.Empty)
            {
                var identity = (ClaimsIdentity)User.Identity;
                var CurrentManagerId = identity.FindFirst(ClaimTypes.NameIdentifier).Value;
                await _service.DeleteAsync(id,CurrentManagerId);
                return Ok();
            }
            return BadRequest();
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] Login login)
        {
            if (ModelState.IsValid)
            {
              var token = await _service.LoginAsync(login.LoginDTO, login.Password);
               if(token == null) 
                    return NotFound();
               return Ok(token);
             
            }
            return NotFound();
        }
        
        [HttpPost("Manager")]
        [Authorize(Roles = "Admin,Organizator")]
        public async Task<IActionResult> RegisterManager([FromBody] ManagerForCreationDTO managerForCreationDTO)
        {
            if (ModelState.IsValid)
            {
                var IsExist = await _service.LoginExist(managerForCreationDTO.Login);
                if (IsExist == null)
                {
                    var identity = (ClaimsIdentity)User.Identity;
                    var currenrtRegion = identity.FindFirst("RegionId").Value;
                    var currentRole = identity.FindFirst(ClaimTypes.Role).Value;
                    managerForCreationDTO.CreatedDate = DateTime.Now;
                    managerForCreationDTO.Creaetedby = Guid
                        .Parse(identity.FindFirst(ClaimTypes.NameIdentifier).Value);
                    var manager = await _service.CreateAsync(managerForCreationDTO, currenrtRegion, currentRole);
                    if (manager != null)
                        return Ok(manager);
                }
            }
            return BadRequest();
        }
    }
}
