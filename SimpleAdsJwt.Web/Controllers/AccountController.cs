using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SimpleAdsJwt.Data;
using SimpleAdsJwt.Web.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SimpleAdsJwt.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private string _connectionString;
        private IConfiguration _configuration;

        public AccountController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConStr");
            _configuration = configuration;
        }

        [HttpPost]
        [Route("signup")]
        public void Signup(SignupViewModel user)
        {
            var repo = new AdRepo(_connectionString);
            repo.AddUser(user, user.Password);
        }

        [HttpPost]
        [Route("login")]
        public IActionResult Login(LoginViewModel viewModel)
        {
            var repo = new AdRepo(_connectionString);
            var user = repo.Login(viewModel.Email, viewModel.Password);
            if (user == null)
            {
                return Unauthorized();
            }

            var claims = new List<Claim>()
            {
                new Claim("user", viewModel.Email)
            };

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetValue<string>("JWTSecret")));
            var credentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var tokenOptions = new JwtSecurityToken(signingCredentials: credentials,
                claims: claims);
            string tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
            return Ok(new { token = tokenString });
        }

        [HttpGet]
        [Route("getcurrentuser")]
        public User GetCurrentUser()
        {
            string email = User.FindFirst("user")?.Value; //get currently logged in users email - this is in place of User.Identity.Name
            if (String.IsNullOrEmpty(email))
            {
                return null;
            }

            var repo = new AdRepo(_connectionString);
            return repo.GetByEmail(email);
        }
    }
}
