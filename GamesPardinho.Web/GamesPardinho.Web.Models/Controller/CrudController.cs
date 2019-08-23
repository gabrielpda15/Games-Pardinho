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
    public abstract class CrudController<TEntity> : GController where TEntity : class, IBaseEntity
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
        [ProducesResponseType(typeof(Exception), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public virtual async Task<IActionResult> GetById(int id, CancellationToken ct)
        {
            try
            {
                return Ok(await Repository.QueryByIdAsync(id, UserContext, ct));
            }
            catch (QueryException) { return NotFound(); }
            catch (Exception ex) { return Internal(ex); }
        }

        [HttpPost]
        [ProducesResponseType(typeof(object), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(Exception), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesDefaultResponseType]
        public virtual async Task<IActionResult> Post([FromBody]TEntity entity, CancellationToken ct)
        {
            try
            {
                var obj = await Repository.AddAsync(entity, UserContext, ct);
                return CreatedAtAction(nameof(Post), obj);
            }
            catch (Exception ex) { return BadRequest(ex); }
        }

        [HttpPost("All")]
        [ProducesResponseType(typeof(IEnumerable<object>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Exception), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesDefaultResponseType]
        public virtual async Task<IActionResult> PostAll([FromBody]IEnumerable<TEntity> entities, CancellationToken ct)
        {
            try
            {
                var obj = await Repository.AddAllAsync(entities, UserContext, ct);
                return Ok(obj);
            }
            catch (Exception ex) { return BadRequest(ex); }
        }

        [HttpPut]
        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Exception), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesDefaultResponseType]
        public virtual async Task<IActionResult> Put([FromBody]TEntity entity, CancellationToken ct)
        {
            try
            {
                return Ok(await Repository.UpdateAsync(entity, UserContext, ct));
            }
            catch (Exception ex) { return BadRequest(ex); }
        }

        [HttpPut("All")]
        [ProducesResponseType(typeof(IEnumerable<object>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Exception), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesDefaultResponseType]
        public virtual async Task<IActionResult> PutAll([FromBody]IEnumerable<TEntity> entities, CancellationToken ct)
        {
            try
            {
                return Ok(await Repository.UpdateAllAsync(entities, UserContext, ct));
            }
            catch (Exception ex) { return BadRequest(ex); }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Exception), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public virtual async Task<IActionResult> Delete(int id, CancellationToken ct)
        {
            try
            {
                if (await Repository.DeleteAsync(id, UserContext, ct)) return Ok();
            }
            catch (QueryException) { return NotFound(); }
            catch (Exception ex) { return BadRequest(ex); }
            return BadRequest();
        }

        [HttpDelete("All/{ids}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Exception), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesDefaultResponseType]
        public virtual async Task<IActionResult> DeleteAll(string ids, CancellationToken ct)
        {
            try
            {
                var result = await Repository.DeleteAllAsync(ids, UserContext, ct);
                if (result == null) return Ok();
                return Ok(result);
            }
            catch (FormatException ex) { return BadRequest(ex); }
        }

    }
}
