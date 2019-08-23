using GamesPardinho.Web.Models.Controller;
using GamesPardinho.Web.Models.Entities.League;
using GamesPardinho.Web.Models.Repository.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GamesPardinho.Web.API.Controllers.League
{
    [Authorize]
    [Route("api/League/[controller]")]
    public class LeagueTeamController : CrudController<LeagueTeam>
    {
        public LeagueTeamController(IUnitOfWork unitOfWork, IUserContext userContext) : base(unitOfWork, userContext)
        {
        }
    }
}
