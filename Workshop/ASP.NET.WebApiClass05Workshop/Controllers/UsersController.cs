using DTOs.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP.NET.WebApiClass05Workshop.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUsersServices _usersServices;

        public UsersController(IUsersServices usersServices)
        {
            _usersServices = usersServices;
        }
        [HttpPost("register")]
        [AllowAnonymous] 
        public IActionResult Register([FromBody] RegisterUserDTO registerUserDto)
        {
            try
            {
                _usersServices.Register(registerUserDto);
                return StatusCode(StatusCodes.Status201Created, "User registered!");
            }
            catch (UserException e)
            {
                return StatusCode(StatusCodes.Status400BadRequest, e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occured!");
            }
        }

        [HttpPost("login")]
        [AllowAnonymous] 
        public ActionResult<string> Login([FromBody] LoginDTO loginDto)
        {
            try
            {
                string token = _usersServices.Login(loginDto);
                return Ok(token);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occured!");
            }
        }
    }
}
