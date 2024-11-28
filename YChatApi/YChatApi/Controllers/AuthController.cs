using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Security.Authentication;
using YChatApi.DTOs;
using YChatApi.Services;

namespace YChatApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login(UserDto loginDto)
        {
            try
            {
                var bearer = await _authenticationService.Login(loginDto);
                return Ok(bearer);
            }
            catch (AuthenticationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register(UserDto loginDto)
        {
            try
            {
                await _authenticationService.Register(loginDto);
            } 
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok("User registered successfully");
        }
    }
}
