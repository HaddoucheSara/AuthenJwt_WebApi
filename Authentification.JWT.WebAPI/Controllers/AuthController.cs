
using Authentification.JWT.DAL.Entities;
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
               
               
                var user = new User()
                {
                    Username = model.Username,
                    Email = model.Email,
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.Password)
                };

                await _userService.RegisterUserAsync(user.Username,user.Email,user.PasswordHash);

                return Ok("User registered successfully.");
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
                // Appeler la méthode LoginUserAsync du service pour valider l'utilisateur
                var userDto = await _userService.LoginUserAsync(model.Username, model.Password);

                // Générer le token JWT en utilisant l'ID de l'utilisateur
                var token = _jwtService.GenerateToken(userDto.Id);

                // Retourner le token dans la réponse
                return Ok(new { Token = token });
            }
            catch (Exception ex)
            {
                // Si une exception est levée (comme "Invalid credentials"), retourner une réponse Unauthorized
                return Unauthorized(ex.Message);
            }
        }

    }


}
