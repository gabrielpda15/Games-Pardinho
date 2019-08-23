using GamesPardinho.Web.Models.Controller;
using GamesPardinho.Web.Models.Entities.Security;
using GamesPardinho.Web.Models.Repository.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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

        [HttpPost]
        [ProducesResponseType(typeof(Identity), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(IdentityResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Exception), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesDefaultResponseType]
        public override async Task<IActionResult> Post([FromBody] Identity entity, CancellationToken ct)
        {
            try
            {
                Repository.OnAdd(entity, UserContext);

                var result = await userManager.CreateAsync(entity, entity.Password);

                if (result.Succeeded) return CreatedAtAction(nameof(Post), entity);
                else return BadRequest(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut]
        [ProducesResponseType(typeof(Identity), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IdentityResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Exception), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesDefaultResponseType]
        public override async Task<IActionResult> Put([FromBody]Identity entity, CancellationToken ct)
        {
            try
            {
                Repository.OnUpdate(entity, UserContext);

                var result = await userManager.UpdateAsync(entity);

                if (result.Succeeded) return Ok(entity);
                else return BadRequest(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

    }
}
