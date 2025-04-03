using Authentification.JWT.Service.Services;
using Authentification.JWT.WebAPI.Logs;
using Authentification.JWT.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Authentication.JWT.WebAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IJwtService _jwtService;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IUserService userService, IJwtService jwtService, ILogger<AuthController> logger)
        {
            _userService = userService;
            _jwtService = jwtService;
            _logger = logger;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            try
            {
                _logger.LogInfo("Attempting to register a new user with username: {Username}", model.Username);
                var userDto = await _userService.RegisterUserAsync(model.Username, model.Email, BCrypt.Net.BCrypt.HashPassword(model.Password));
                _logger.LogInfo("User {Username} registered successfully.", model.Username);
                return Ok(new { Message = "User registered successfully.", User = userDto });
            }

            catch (Exception ex)
            {
                _logger.LogErr(ex, "An error occurred while registering user {Username}.", model.Username);
                return BadRequest(ex.Message);
            }

           
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            try
            {
                _logger.LogInfo("Attempting to login user with username: {Username}", model.Username);
                var userDto = await _userService.LoginUserAsync(model.Username, model.Password);
                var token = _jwtService.GenerateToken(userDto.Id);

                _logger.LogInfo("User {Username} logged in successfully.", model.Username);
                return Ok(new { Token = token });
            }
            catch (Exception ex)
            {
                _logger.LogErr(ex, "An error occurred during login for user {Username}.", model.Username);
                return Unauthorized(ex.Message);
            }
        }

    }


}
