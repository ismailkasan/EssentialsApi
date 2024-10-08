﻿using Essentials.Application;
using Essentials.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Essentials.Api.Controllers
{
    [Route("api/V1/[controller]")]
    [ApiController]
    public class TokenController(
        IUserService userService,
        IConfiguration configuration
        ) : ControllerBase
    {
        private readonly IConfiguration _configuration = configuration;
        private readonly IUserService _userService = userService;

        [AllowAnonymous]
        [HttpPost]
        [Route("token")]
        public IActionResult Post([FromBody] UserInputModel request)
        {
            if (ModelState.IsValid)
            {
                var user = _userService.Login(request);
                if (user == null)
                {
                    return Unauthorized();
                }

                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, request.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                var token = new JwtSecurityToken
                (
                    issuer: _configuration["Issuer"], //appsettings.json içerisinde bulunan issuer değeri
                    audience: _configuration["Audience"],//appsettings.json içerisinde bulunan audince değeri
                    claims: claims, // yetkilendirme için claim ler
                    expires: DateTime.UtcNow.AddDays(30), // 30 gün geçerli olacak                
                    signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["SigningKey"])),//appsettings.json içerisinde bulunan signingkey değeri
                            SecurityAlgorithms.HmacSha256)
                );
                return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
            }
            return BadRequest();
        }


        [AllowAnonymous]
        [HttpGet]
        [Route("Test")]
        public IActionResult Test()
        {
            return Ok("Test başarılı linux github-2");
        }
    }
}
