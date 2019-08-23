using GamesPardinho.Web.Models.Entities.League;
using GamesPardinho.Web.Models.Repository.Base;
using GamesPardinho.Web.Models.Repository.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GamesPardinho.Web.Models.Repository.Repositories.League
{
    [Repository(typeof(LeagueAccount))]
    public class LeagueAccountRepository : BaseRepository<LeagueAccount, ModelDbContext>
    {
        public LeagueAccountRepository(ModelDbContext context) : base(context)
        {
        }

        protected override IQueryable<LeagueAccount> GetEntities()
        {
            return Entities.Include(x => x.Identity).Include(x => x.SummonerName);
        }
    }
}
