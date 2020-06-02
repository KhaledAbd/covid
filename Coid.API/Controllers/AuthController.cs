using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Coid.API.Data;
using Coid.API.Dtos;
using Coid.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Coid.API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository repository;
        private readonly IConfiguration configuration;
        private readonly IMapper mapper;

        public AuthController(IAuthRepository repository, IConfiguration configuration, IMapper mapper)
        {
            this.mapper = mapper;
            this.configuration = configuration;
            this.repository = repository;
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserForLoginDto userForLogin)
        {
            var userFromRepo = await repository.Login(userForLogin.username.ToLower(), userForLogin.password);
            if (userFromRepo == null)
                return Unauthorized();

            var claims = new[] {
                new Claim (ClaimTypes.NameIdentifier, userFromRepo.Id.ToString()),
                new Claim(ClaimTypes.Name, userFromRepo.Username)
                ,new Claim(ClaimTypes.Role, userFromRepo.typePerson.ToString())
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(configuration.GetSection("Appsettings:Token").Value));
            var creds = new SigningCredentials(key,
            SecurityAlgorithms.HmacSha512Signature);
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var user = mapper.Map<UserForDetailsDto>(userFromRepo);
            user.Token = tokenHandler.WriteToken(token);
            return Ok(user);
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserForRegisterDto userForRegister)
        {
            userForRegister.username = userForRegister.username.ToLower();
            if (await repository.UserExists(userForRegister.username))
            {
                return BadRequest("User Is Exist ...");
            }
            var username = new User()
            {
                Username = userForRegister.username
            };
            var createdUser = await repository.Register(username, userForRegister.password);
            return StatusCode(201);
        }
    }
}