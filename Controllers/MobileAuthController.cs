using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MasterThesisWebApplication.Data.Interfaces;
using MasterThesisWebApplication.Dtos;
using MasterThesisWebApplication.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace MasterThesisWebApplication.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class MobileAuthController : ControllerBase
    {
        private readonly IMobileAuthRepository _repo;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;

        public MobileAuthController(IMobileAuthRepository repo, IConfiguration config, IMapper mapper)
        {
            _repo = repo;
            _config = config;
            _mapper = mapper;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] MobileUserForRegisterDto mobileUserForRegisterDto)
        {
            mobileUserForRegisterDto.Username = mobileUserForRegisterDto.Username.ToLower();

            if (await _repo.UserExists(mobileUserForRegisterDto.Username))
            {
                return BadRequest("Username already exists");
            }

            var userToCreate = _mapper.Map<MobileUser>(mobileUserForRegisterDto);

            var createdUser = await _repo.Register(userToCreate, mobileUserForRegisterDto.Password);
            
            return Ok(createdUser);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] MobileUserForLoginDto mobileUserForLoginDto)
        {
            var user = await _repo.Login(mobileUserForLoginDto.Username.ToLower(), mobileUserForLoginDto.Password);

            if (user == null)
            {
                return Unauthorized();
            }

            return Ok(new
            {
                token = GenerateJwtToken(user)
            });
        }

        private string GenerateJwtToken(MobileUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username)
            };

            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_config.GetSection("AppSettings:TokenKey").Value));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(50),
                SigningCredentials = credentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}