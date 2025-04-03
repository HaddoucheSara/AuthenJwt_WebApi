using Authentification.JWT.Service.Services;
using Authentification.JWT.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace Authentication.JWT.WebAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IJwtService _jwtService;

        public AuthController(IUserService userService, IJwtService jwtService)
        {
            _userService = userService;
            _jwtService = jwtService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            try
            {
               
               
                var userDto = await _userService.RegisterUserAsync(model.Username, model.Email, BCrypt.Net.BCrypt.HashPassword(model.Password));
                return Ok(new { Message = "User registered successfully.", User = userDto });
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

           
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            try
            {
               
                var userDto = await _userService.LoginUserAsync(model.Username, model.Password);

                
                var token = _jwtService.GenerateToken(userDto.Id);

            
                return Ok(new { Token = token });
            }
            catch (Exception ex)
            {
               
                return Unauthorized(ex.Message);
            }
        }

    }


}
