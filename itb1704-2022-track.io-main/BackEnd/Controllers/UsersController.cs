using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BackEnd.Model;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Test.itb1704.BackEnd.Model;

namespace BackEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly DataContext _context;
        private IConfiguration _config;

        public UsersController(IConfiguration config, DataContext context)
        {
            _config = config;
            _context = context;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] User login)
        {
            var dbUser = _context.UserList!.FirstOrDefault(user => user.Username == login.Username);

            if (dbUser == null) return NotFound();

            if (dbUser.Password != HashPassword(login.Password)) return Unauthorized();

            var token = GenerateJSONWebToken(dbUser);

            return Ok(new { token = token });

        }

        [HttpGet]
        public IActionResult GetUserInfo()
        {
            var dbUser = _context.UserList!.Where(x => x.Id == GetUserId());

            if (dbUser == null)
            {
                return BadRequest();
            }

            return Ok(dbUser);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateUserInfo(int id, [FromBody] User user)
        {
            var dbUser = _context.UserList!.AsNoTracking().FirstOrDefault(x => x.Id == user.Id);
            if (id != user.Id || dbUser == null)
            {
                return NotFound();
            }

            if (dbUser.Id != user.Id)
            {
                return Unauthorized();
            }

            _context.Update(user);
            _context.SaveChanges();

            return Ok(user);
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] User user)
        {
            var dbUser = _context.UserList?.Find(user.Id);
            if (dbUser == null)
            {
                var maxId = 0;
                user.Password = HashPassword(user.Password);
                var orgId = _context.UserList!;
                orgId.Where(x => x.OrganizationId != 0);
                foreach (var id in orgId)
                {
                    if (id.OrganizationId >= maxId)
                    {
                        maxId = id.OrganizationId;
                    }
                }
                user.OrganizationId = maxId + 1;
                _context.Add(user);
                _context.SaveChanges();

                return CreatedAtAction("Register", new { Id = user.Id }, user);
            }
            else
            {
                return Conflict();
            }
        }

        private string HashPassword(string password)
        {
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: new byte[0],
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8));

            return hashed;
        }

        private string GenerateJSONWebToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Issuer"],
              new List<Claim> { new Claim("organizationId", user.OrganizationId.ToString()), new Claim("Id", user.Id.ToString()) },
              expires: DateTime.Now.AddMinutes(10),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private int GetUserId()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity.Claims.Count() > 0)
            {
                return int.Parse(identity!.FindFirst("Id")!.Value);
            }
            return 0;
        }
    }
}