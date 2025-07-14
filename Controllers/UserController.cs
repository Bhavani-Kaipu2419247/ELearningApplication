using ELearningApplication.API.Infrastructure;
using ELearningApplication.API.Models;
using ELearningApplication.API.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ELearningApplication.API.Controllers
{
    [TypeFilter(typeof(CustomExceptionFilter))]
    [Route("api/[controller]")]
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
        public async Task<ActionResult<User>> RegisterUser(User user)
        {
            if (await _userRepository.UserExistsAsync(user.Email))
                return BadRequest("Email already registered.");

            var registeredUser = await _userRepository.RegisterUserAsync(user);
            return CreatedAtAction(nameof(GetUser), new { id = registeredUser.UserId }, registeredUser);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login([FromBody] Login login)
        {
            var user = _userRepository.Authenticate(login.Email, login.Password);
            if (user != null)
            {
                var token = GenerateJwtToken(login);
                return Ok(new {token=token});
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

        [HttpGet("{id}")]
        //[Authorize(Roles ="student || instructor")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            return user == null ? NotFound() : Ok(user);
        }

        [HttpPut("{id}")]
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

    }
}
