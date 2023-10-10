using ContactAppAPI.Application.DTO;
using ContactAppAPI.Application.Implementation.Interface;
using ContactAppAPI.Application.Implementation.Services;
using ContactAppAPI.Domain.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ContactAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        ILoginServices _loginService; IRegisterServices _registerService;
        public AuthController(ILoginServices loginService, IRegisterServices registerService)
        {
            _loginService = loginService; _registerService = registerService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            string response = await _loginService.Login(loginDto);
            if (response == "Email not found")
            {
                return NotFound(new { Message = "Invalid email. Please check and try again." });
            }
            else if (response == "Invalid credentials")
            {
                return Unauthorized(new { Message = "Invalid credentials" });
            }

            return Ok(new { Token = response });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegDto regDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            bool response = await _registerService.CreateContact(regDto);
            if (!response)
            {
                return BadRequest(ModelState);
            }
            return Ok(new
            {
                Message = "Contact Registration Successful"
            });
        }
    }
}
