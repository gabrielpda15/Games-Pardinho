using GamesPardinho.Web.Models.Entities.Base;
using GamesPardinho.Web.Models.Repository.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GamesPardinho.Web.Models.Controller
{
    [Authorize]
    [Route("api/[controller]")]
    public abstract class CrudController<TEntity> : Microsoft.AspNetCore.Mvc.Controller where TEntity : class, IBaseEntity
    {
        protected IAsyncRepository<TEntity> Repository { get; }
        protected IUserContext UserContext { get; }

        protected CrudController(IUnitOfWork unitOfWork, IUserContext userContext)
        {
            Repository = unitOfWork.GetRepository<TEntity>();
            UserContext = userContext;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IList<object>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesDefaultResponseType]
        public virtual async Task<IActionResult> Get(CancellationToken ct)
        {
            return Ok(await Repository.QueryAsync(x => x, UserContext, ct: ct));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public virtual async Task<IActionResult> GetById(int id, CancellationToken ct)
        {
            try
            {
                return Ok(await Repository.QueryByIdAsync(id, UserContext, ct));
            }
            catch { return NotFound(); }
        }

        [HttpPost]
        [ProducesResponseType(typeof(object), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public virtual async Task<IActionResult> Post([FromBody]TEntity entity, CancellationToken ct)
        {
            try
            {
                var obj = await Repository.AddAsync(entity, UserContext, ct);
                return CreatedAtAction(nameof(Post), obj);
            }
            catch { return BadRequest(); }
        }
    }
}
