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
using System.Net.Http.Headers;
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
        protected RoleManager<Role> RoleManager { get; }
        protected AuthConfig AuthConfig { get; }

        public LoginController(UserManager<Identity> userManager, RoleManager<Role> roleManager, AuthConfig authConfig)
        {
            this.UserManager = userManager;
            this.RoleManager = roleManager;
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

        [Authorize]
        [HttpPost("ValidateById/{id}")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> ValidateById(int id)
        {
            var role = await RoleManager.FindByIdAsync(id.ToString());

            if (role == null)
                return NotFound();

            return await ValidateByName(role.Name);
        }

        [Authorize]
        [HttpPost("Validate/{role}")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> ValidateByName(string role)
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                if (await RoleManager.RoleExistsAsync(role))
                {
                    var user = await UserManager.FindByNameAsync(HttpContext.User.Claims.FirstOrDefault().Value);

                    var roles = await UserManager.GetRolesAsync(user);

                    if (roles.Contains(role))
                    {
                        return Accepted();
                    }
                }                
            }
            
            return BadRequest();
        }

        [HttpPost("InitialCreate")]
        [ProducesResponseType(typeof(IDictionary<string, IdentityResult>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(IDictionary<string, IdentityResult>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Exception), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> InitialCreate([FromServices]ModelDbContext context)
        {
            var resultList = new Dictionary<string, IdentityResult>(); 

            if (!context.Roles.Any())
            {
                Role admin = new Role()
                {
                    Name = "Administrador"
                };
                Role user = new Role()
                {
                    Name = "Usuario"
                };

                try
                {
                    resultList.Add("RoleAdmin", await RoleManager.CreateAsync(admin));
                    resultList.Add("RoleUser", await RoleManager.CreateAsync(user));
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }
                
            }

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
                    resultList.Add("IdentityAdmin", await UserManager.CreateAsync(user, user.Password));
                    resultList.Add("IdentityToRole", await UserManager.AddToRoleAsync(user, "Administrador"));
                } 
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }
                
            }

            if (resultList.Count > 0)
            {
                var hasSuccess = true;
                foreach (var key in resultList.Keys)
                {
                    if (!resultList[key].Succeeded) hasSuccess = false;
                }

                if (hasSuccess)
                {
                    return CreatedAtAction(nameof(InitialCreate), resultList);
                }
                else
                {
                    return BadRequest(resultList);
                }
            }            

            return Ok();
        }


    }
}
