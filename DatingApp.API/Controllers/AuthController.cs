using System.Threading.Tasks;
using Datingapp.API.Data;
using Datingapp.API.Models;
using Microsoft.AspNetCore.Mvc;
using Datingapp.API.Dtos;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System;
using Microsoft.Extensions.Configuration;
namespace Datingapp.API.Controllers
{
    [Route("api/auth")]
    public class AuthController : Controller
    {
        private readonly IAuthRepository _repo;
        private readonly IConfiguration _config;
        public AuthController(IAuthRepository repo, IConfiguration config)
        //public AuthController(IAuthRepository repo)
        {
            this._config = config;
            this._repo = repo;

        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]UserForRegisterDto userForRegisterDto)
        {
            if(string.IsNullOrEmpty(userForRegisterDto.UserName)==false)
            userForRegisterDto.UserName = userForRegisterDto.UserName.ToLower();

            if (await _repo.UserExists(userForRegisterDto.UserName))
                ModelState.AddModelError("username", "user name already exists");


            //validate request
            if (!ModelState.IsValid)
                return BadRequest(ModelState);




            var userToCreate = new User
            {
                UserName = userForRegisterDto.UserName
            };

            var createUser = await _repo.Register(userToCreate, userForRegisterDto.Password);

            return StatusCode(201);
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] UserForLoginDto userForLoginDto)
        {

            throw new Exception("computer says no!");     
            var userFromRepo = await _repo.Login(userForLoginDto.Username.ToLower(), userForLoginDto.Password);
            if (userFromRepo == null)
                return Unauthorized();

            //generate token
            var tokenHandler = new JwtSecurityTokenHandler();
            //var key = Encoding.ASCII.GetBytes("Super secret key");
            var key=Encoding.ASCII.GetBytes(_config.GetSection("AppSetting:Token").Value);
                        var tokenDescriptor = new SecurityTokenDescriptor
                            {
                                Subject = new System.Security.Claims.ClaimsIdentity(new Claim[]
                                 {
                          new Claim(ClaimTypes.NameIdentifier,userFromRepo.Id.ToString()),
                          new Claim(ClaimTypes.Name,userFromRepo.UserName)
                                     //new Claim(ClaimTypes.Name,userFromRepo.UserName)
                                 }),

                                Expires = DateTime.Now.AddDays(1),
                                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                                      SecurityAlgorithms.HmacSha512Signature)
                            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);
            return Ok(new { tokenString });
           
            
        }

    }
}