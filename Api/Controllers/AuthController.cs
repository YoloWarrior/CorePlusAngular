using Api.Data;
using Api.Dtos;
using Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Api.Controllers
{

    [Route("api/[controller]")]
    
    [ApiController]
    public class AuthController:ControllerBase
    {
        private readonly IAuthRepository _repos;

        private readonly IConfiguration _config;

        public AuthController(IAuthRepository authRepository,IConfiguration config)
        {
            _repos = authRepository;
            _config = config;
        }
        [HttpPost("register")]
        public async Task<IActionResult>Register([FromBody]UserForRegisterDTO userForRegisterDTO)
        {
            
            userForRegisterDTO.Username = userForRegisterDTO.Username.ToLower();
            if (await _repos.UserExists(userForRegisterDTO.Username))
                return BadRequest("Already have this username");

            var CreateUser = new User
            {
                UserName = userForRegisterDTO.Username
            };
            var CreatedUser = await _repos.Register(CreateUser, userForRegisterDTO.Password);
            return StatusCode(201);
        }
        [HttpPost("login")]
        public async Task<IActionResult>Login(UserForLoginDTO userForLoginDTO)
        {
            var logUser = await _repos.Login(userForLoginDTO.Username.ToLower(), userForLoginDTO.Password);

            if (logUser == null)
                return Unauthorized();

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier,logUser.Id.ToString()),
                new Claim(ClaimTypes.Name,userForLoginDTO.Username),

            };
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };
            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return Ok(new {
                token = tokenHandler.WriteToken(token)
            });
        }

    }
}
