using ELearningApplication.API.Infrastructure;
using ELearningApplication.API.Models;
using ELearningApplication.API.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ELearningApplication.API.Controllers
{
    [TypeFilter(typeof(CustomExceptionFilter))]
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private IConfiguration Configuration { get; }

        public UsersController(IUserRepository userRepository, IConfiguration Configuration)
        {
            _userRepository = userRepository;
            this.Configuration = Configuration;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public  async Task<IActionResult> RegisterUser([FromBody] User user)
        {
            if (ModelState.IsValid)
            {
                //Debug.WriteLine($"api {user}");
                // if (await _userRepository.UserExistsAsync(user.Email))
                //     return BadRequest("Email already registered.");

                var registeredUser = await _userRepository.RegisterUserAsync(user);
                return CreatedAtAction(nameof(GetUser), new { id = registeredUser.UserId }, registeredUser);
            }
            else
            {
                return BadRequest();
            }
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login([FromBody] Login login)
        {
            var user = _userRepository.Authenticate(login.Email, login.Password);
            if (user != null)
            {
                var token = GenerateJwtToken(login);
                return Ok(new { token = token });
            }
            return BadRequest();

        }
        private string GenerateJwtToken(Login user)
        {
            var issuer = Configuration["Jwt:Issuer"];
            var audience = Configuration["Jwt:Audience"];
            var claims = new List<Claim>
             {
                 new Claim(ClaimTypes.Email, user.Email)
             };
            var key = Convert.FromBase64String(Configuration["Jwt:Key"]);
            var securityKey = new SymmetricSecurityKey(key);
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(1),
                signingCredentials: credentials
            );
            var encodedToken = new JwtSecurityTokenHandler().WriteToken(token);

            return encodedToken;
        }

        [HttpGet("{id:int}")]
        //[Authorize(Roles ="student || instructor")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            return user == null ? NotFound() : Ok(user);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] User updatedUser)
        {
            if (id != updatedUser.UserId)
                return BadRequest();

            await _userRepository.UpdateUserAsync(updatedUser);
            return Ok("Updated Successfully!");
        }
        [HttpGet("trigger-error")]
        public IActionResult TriggerError()
        {
            int number = 10;
            int zero = 0;

            // This will throw a DivideByZeroException
            int result = number / zero;


            return Ok("Divide by Zero Exception");
        }

        [AllowAnonymous]
        [HttpOptions]
        [Route("preflight")]
        public IActionResult Preflight()
        {
            return NoContent();
        }

        // public IActionResult Preflight()
        // {
        //     Response.Headers.Add("Access-Control-Allow-Origin", "https://legendary-adventure-jjgg9wrpr97q2q7jg-5173.app.github.dev");
        //     Response.Headers.Add("Access-Control-Allow-Headers", "Content-Type, Authorization");
        //     Response.Headers.Add("Access-Control-Allow-Methods", "POST, OPTIONS");
        //     return Ok();
        // }

    }
}
