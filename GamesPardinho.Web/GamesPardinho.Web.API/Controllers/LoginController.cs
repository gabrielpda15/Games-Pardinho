using GamesPardinho.Web.Models.Entities.Security;
using GamesPardinho.Web.Models.Repository.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace GamesPardinho.Web.API.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    public class LoginController : Controller
    {
        protected UserManager<Identity> UserManager { get; }
        protected AuthConfig AuthConfig { get; }

        public LoginController(UserManager<Identity> userManager, AuthConfig authConfig)
        {
            this.UserManager = userManager;
            this.AuthConfig = authConfig;
        }

        [HttpPost]
        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Exception), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Login([FromBody]User user)
        {
            var identity = await UserManager.FindByNameAsync(user.Username);
            if (identity != null && await UserManager.CheckPasswordAsync(identity, user.Password))
            {
                try
                {
                    var authClaims = new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, user.Username),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                    };

                    var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AuthConfig.SecretKey));

                    var token = new JwtSecurityToken(
                        issuer: AuthConfig.Issuer,
                        audience: AuthConfig.Audience,
                        notBefore: DateTime.Now,
                        expires: DateTime.Now + TimeSpan.FromSeconds(AuthConfig.ExpirationSeconds),
                        claims: authClaims,
                        signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                        );

                    var handler = new JwtSecurityTokenHandler();

                    var obj = new
                    {
                        Token = handler.WriteToken(token),
                        Creation = token.ValidFrom,
                        Expiration = token.ValidTo
                    };

                    return Ok(obj);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }
            }

            return Unauthorized();
        }

        [HttpPost("InitialCreate")]
        [ProducesResponseType(typeof(IdentityResult), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(IdentityResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Exception), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> InitialCreate([FromServices]ModelDbContext context)
        {
            if (!context.Users.Any())
            {
                Identity user = new Identity()
                {
                    Email = "admin@localhost",
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = "admin",
                    Password = "Admin!102030"
                };

                try
                {
                    var result = await UserManager.CreateAsync(user, user.Password);

                    if (result.Succeeded) return CreatedAtAction(nameof(InitialCreate), result);
                    else return BadRequest(result);
                } 
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }
                
            }

            return Ok();
        }


    }
}
