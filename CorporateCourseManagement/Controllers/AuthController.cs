using CorporateCourseManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace CorporateCourseManagement.Controllers
{
    public class AuthController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly CorporateCourseManagementDbContext _context;

        public AuthController(IConfiguration configuration, CorporateCourseManagementDbContext context)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("register")]
        public async Task<ActionResult<User>> Register([FromBody] User user)
        {
            var CheckUser = await _context.Users.FirstOrDefaultAsync(e => e.EmailId == user.EmailId);

            if (CheckUser == null) {
                if (user != null && user.Password.Length > 8 && user.EmailId.Contains("@gmail.com"))
                {
                    _context.Users.Add(user);
                    await _context.SaveChangesAsync();
                    return Ok("User Created Successfully");
                }
                else
                {
                    return BadRequest("Incorrect input");
                }
            }
            else
            {
                return BadRequest("User already exist");
            }
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<User>> Login([FromBody] Login user)
        {
            var dbUser = await _context.Users.FirstOrDefaultAsync(x => x.EmailId == user.EmailId && x.Password == user.Password);
            if (dbUser == null)
            {
                return BadRequest("User Not Found");
            }
            string token = CreateToken(user);
            return Ok(token);
        }

        private string CreateToken(Login user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.EmailId),
                new Claim(ClaimTypes.Role,"1")
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }
}
