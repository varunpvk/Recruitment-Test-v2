namespace JG.FinTechTest.Controllers
{
    using JG.FinTech.Models;
    using JG.FinTechTest.Routes;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Microsoft.IdentityModel.Tokens;
    using System;
    using System.Collections.Generic;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;

    [Route(FinTechRoutes.basePath)]
    [ApiController]
    public class DonorsController : BaseController
    {
        private readonly IConfiguration config;
        public DonorsController(IConfiguration config)
        {
            this.config = config;
        }

        /// <summary>
        /// Register Donor.
        /// </summary>
        /// <param name="loginModel"></param>
        /// <returns></returns>
        [HttpPost]
        [Route(FinTechRoutes.donorsLoginPath)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [AllowAnonymous]
        public IActionResult Login(LoginModel loginModel)
        {
            IActionResult response = Unauthorized();

            if(!loginModel.Equals(default))
            {
                var tokenString = GenerateJSONWebTokenAsync(loginModel);
                response = Ok(new
                {
                    Token = tokenString,
                    Message = "Success"
                });
            }

            return response;
        }

        /// <summary>
        /// Retrieve the bearer token
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route(FinTechRoutes.tokenPath)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize]
        public async Task<IEnumerable<string>> Get()
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");

            return new string[] { accessToken };
        }
        

        /// <summary>
        /// Generate JSon WebToken
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        private string GenerateJSONWebTokenAsync(LoginModel userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new List<Claim>();
            claims.Add(new Claim("Name", userInfo.UserName));
            claims.Add(new Claim("PostCode", userInfo.PostCode));

            var token = new JwtSecurityToken(config["Jwt:Issuer"], config["Jwt:Issuer"], claims, expires: DateTime.Now.AddMinutes(120), signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);

        }
    }
}