using GamesPardinho.Web.Models.Entities.League;
using GamesPardinho.Web.Models.Repository.Base;
using GamesPardinho.Web.Models.Repository.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace GamesPardinho.Web.Models.Repository.Repositories.League
{
    [Repository(typeof(LeagueAccount))]
    public class LeagueAccountRepository : BaseRepository<LeagueAccount, ModelDbContext>
    {
        public LeagueAccountRepository(ModelDbContext context) : base(context)
        {
        }
    }
}
