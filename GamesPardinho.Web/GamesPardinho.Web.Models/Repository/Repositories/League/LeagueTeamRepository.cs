using GamesPardinho.Web.Models.Entities.League;
using GamesPardinho.Web.Models.Repository;
using GamesPardinho.Web.Models.Repository.Base;
using GamesPardinho.Web.Models.Repository.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GamesPardinho.Web.Models.Repository.Repositories
{
    [Repository(typeof(LeagueTeam))]
    public class LeagueTeamRepository : BaseRepository<LeagueTeam, ModelDbContext>
    {
        public LeagueTeamRepository(ModelDbContext context) : base(context)
        {

        }
    }
}
