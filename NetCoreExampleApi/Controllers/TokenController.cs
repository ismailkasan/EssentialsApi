// <copyright file="TokenController.cs" company="TC Sağlık Bakanlığı">
// Copyright(c) 2021 Tüm Hakları Saklıdır
// </copyright>
// <author>İsmail KAŞAN</author>
// <date>3.07.2021</date>

using ApplicationExample;
using DomainExample;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NetCoreExampleApi.Controllers
{
    /// <summary>
    /// Defines the <see cref="TokenController" />.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        /// <summary>
        /// Defines the _configuration.
        /// </summary>
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Defines the _userService.
        /// </summary>
        private readonly IUserService _userService;

        /// <summary>
        /// Initializes a new instance of the <see cref="TokenController"/> class.
        /// </summary>
        /// <param name="userService">The userService<see cref="IUserService"/>.</param>
        /// <param name="configuration">The configuration<see cref="IConfiguration"/>.</param>
        public TokenController(IUserService userService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
        }

        /// <summary>
        /// The Post.
        /// </summary>
        /// <param name="request">The request<see cref="UserInputDto"/>.</param>
        /// <returns>The <see cref="IActionResult"/>.</returns>
        [AllowAnonymous]
        [HttpPost]
        [Route("token")]
        public IActionResult Post([FromBody] UserInputDto request)
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
            return Ok("Test başarılı linux");
        }
    }
}
