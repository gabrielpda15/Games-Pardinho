using GamesPardinho.Web.Models.Repository.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GamesPardinho.Web.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class TestController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return await Task.FromResult(Ok(new { Result = "Ok!" }));
        }

        [AllowAnonymous]
        [HttpGet("anonymous")]
        public async Task<IActionResult> GetAnonymous()
        {
            return await Task.FromResult(Ok(new { User.Identity.IsAuthenticated }));
        }

        [AllowAnonymous]
        [HttpGet("unitofwork")]
        public async Task<IActionResult> GetUnitOfWork([FromServices]IUnitOfWork unitOfWork)
        {
            return await Task.FromResult(Ok());
        }
    }
}
