using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecommerce.API.Helper;
using Ecommerce.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Ecommerce.API.Controllers
{
    [ApiController]
    public class AccountController : ControllerBase
    {       
        private IConfiguration _config;
        private readonly ApplicationDbContext _db;

        public AccountController(ApplicationDbContext db,IConfiguration config)
        {
            _db = db;
            _config = config;
        }

        [Route("api/Account/UserLogin")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody]UserModel login)
        {
            try
            {
                string password = EncryptDecrypt.Encrypt(login.Password);
                var user = await _db.Users.FirstOrDefaultAsync(x => x.Email == login.Email && x.Password == password); 

                if (user != null)
                {
                    var Authtoken = GenerateJSONWebToken(user);
                    user.UserToken = Authtoken;
                    _db.Users.Update(user);
                    await _db.SaveChangesAsync();
                    return Ok(new { Message = "Login Successfully", token = Authtoken });
                }
                else
                {
                    return BadRequest(new { Message = "Email and Password Incorrect!" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }                       
        }

        private string GenerateJSONWebToken(UserViewModel userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],_config["Jwt:Issuer"],null,
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}