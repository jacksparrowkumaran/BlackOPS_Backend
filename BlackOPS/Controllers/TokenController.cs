﻿using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BlackOPS.Models.Login;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace BlackOPS.Controllers
{
    [Route("api/Token")]
    public class TokenController : Controller
    {
        private IConfiguration _config;

        public TokenController(IConfiguration config)

        {

            _config = config;

        }
        [AllowAnonymous]

        [HttpPost]

        public dynamic Post([FromBody]LoginViewModel login)

        {

            IActionResult response = Unauthorized();

            var user = Authenticate(login);

            if (user != null)

            {

                var tokenString = BuildToken(user);

                response = Ok(new { token = tokenString });

            }

            return response;

        }

        private string BuildToken(UserViewModel userInfo)

        {

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub, userInfo.UserName),
                new Claim(JwtRegisteredClaimNames.Email, userInfo.Email)
            };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],

              _config["Jwt:Issuer"],

              claims,

              expires: DateTime.Now.AddMinutes(30),

              signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);

        }

        private UserViewModel Authenticate(LoginViewModel login)

        {

            UserViewModel user = null;

            if (login.Username == "admin" && login.Password == "admin")

            {

                user = new UserViewModel { UserName = "Pablo", Email = "admin@gmail.com" };

            }

            return user;

        }
    }
}