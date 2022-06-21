﻿using AutoMapper;
using Business.Interface;
using Business.ModelDTO;
using Infrastructure.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> GetPageList([FromQuery] int page, int pageSize)
        {
            if (page <= 0 || pageSize <= 0) return NotFound();

            var managers = await _service.GetPageListAsync(page, pageSize);

            if (managers == null) return NotFound();

            return Ok(managers);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            if (!id.Equals(Guid.Empty))
            {
                var manager = await _service.GetManagerAsync(id);
                if (manager != null)
                    return Ok(manager);
            }
            return NotFound();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] ManagerDTO managerDTO)
        {
            if (ModelState.IsValid)
            {
               var manager =  await _service.UpdateAsync(managerDTO);
                if (manager != null)
                    return Ok(manager);
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id != Guid.Empty)
            {
                await _service.DeleteAsync(id);
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
                    var manager = await _service.CreateAsync(managerForCreationDTO, currenrtRegion, currentRole);
                    if (manager != null)
                        return Ok(manager);
                }
            }
            return BadRequest();
        }
    }
}