using GamesPardinho.Web.Models.Entities.League;
using GamesPardinho.Web.Models.Repository;
using GamesPardinho.Web.Models.Repository.Base;
using GamesPardinho.Web.Models.Repository.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GamesPardinho.Web.Models.Repository.Repositories.League
{
    [Repository(typeof(LeagueTournament))]
    public class LeagueTournamentRepository : BaseRepository<LeagueTournament, ModelDbContext>
    {
        public LeagueTournamentRepository(ModelDbContext context) : base(context)
        {

        }
    }
}
