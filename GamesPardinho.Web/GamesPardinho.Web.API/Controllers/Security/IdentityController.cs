using GamesPardinho.Web.Models.Controller;
using GamesPardinho.Web.Models.Entities.Security;
using GamesPardinho.Web.Models.Repository.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace GamesPardinho.Web.API.Controllers.Security
{
    [Authorize]
    [Route("api/security/[controller]")]
    public class IdentityController : CrudController<Identity>
    {
        private UserManager<Identity> userManager;
        public IdentityController(IUnitOfWork unitOfWork, IUserContext userContext, UserManager<Identity> userManager) : base(unitOfWork, userContext)
        {
            this.userManager = userManager;
        }

        public override async Task<IActionResult> Post([FromBody] Identity entity, CancellationToken ct)
        {
            try
            {                
                entity.CreationDate = DateTime.Now;
                entity.EditionDate = DateTime.Now;
                entity.CreationIp = UserContext.IP;
                entity.EditionIp = UserContext.IP;
                entity.CreationUser = ((ClaimsPrincipal)UserContext.Principal).Claims.FirstOrDefault().Value;
                entity.EditionUser = ((ClaimsPrincipal)UserContext.Principal).Claims.FirstOrDefault().Value;

                var result = await userManager.CreateAsync(entity, entity.Password);

                if (result.Succeeded) return CreatedAtAction(nameof(Post), entity);
                else return BadRequest(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

    }
}
