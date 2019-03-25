using Api.Data;
using Api.Dtos;
using Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
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
                UserName = userForRegisterDTO.Username,
                Email = userForRegisterDTO.Email,
                IsConfirm = false
                
            };
            var CreatedUser = await _repos.Register(CreateUser, userForRegisterDTO.Password);
        
            var callbackUrl = Url.Action(
                   "ConfirmEmail",
                   "auth",
                   new {CreatedUser.UserName },
                   protocol: HttpContext.Request.Scheme);
            EmailService emailService = new EmailService();
            await emailService.SendEmailAsync(userForRegisterDTO.Email, "Confirm your account",
                $"{CreateUser.UserName} Спасибо за регистрацию.Для завершения перейдите по ссылке <a href='{callbackUrl}'>link</a>");
            return StatusCode(201);
            
          
           
        }

        public async Task<bool> IsConfirm(string username)
        {
            bool isgood = false;
            var users = await _repos.GetUser(username);
            return  isgood = users.IsConfirm;
        }
        
        [HttpGet]
        [AllowAnonymous]
        public async Task <ActionResult> ConfirmEmail(string username)
        {
           var user= await _repos.GetUser(username);
            if (user!=null)
            {
                    user.IsConfirm = true;
                _repos.Update(user);
                
            }

            return Redirect("https://localhost:4200/");


        }
        


        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult>Login([FromBody]UserForLoginDTO userForLoginDTO)
        {
          
            var logUser = await _repos.Login(userForLoginDTO.Username.ToLower(), userForLoginDTO.Password);
            
            if (logUser == null)
                return Unauthorized();
            var userConfirm = await IsConfirm(userForLoginDTO.Username);
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier,logUser.Id.ToString()),
                new Claim(ClaimTypes.Name,userForLoginDTO.Username),
                new Claim("IsConfirm",userConfirm.ToString().ToLower())

            };
            

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512Signature);

            

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddHours(1),
                SigningCredentials = creds
            };
            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);
            //if (await userConfirm == false)
            //{
              
            //    return Content("Пожалуйста подтвердите email адрес");
                
            

            return Ok(new {
                token = tokenHandler.WriteToken(token)
            });
        }

    }
}
