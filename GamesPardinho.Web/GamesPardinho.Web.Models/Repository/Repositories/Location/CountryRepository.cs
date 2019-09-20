using GamesPardinho.Web.Models.Entities.Location;
using GamesPardinho.Web.Models.Repository.Base;
using GamesPardinho.Web.Models.Repository.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GamesPardinho.Web.Models.Repository.Repositories.Location
{
    [Repository(typeof(Country))]
    public class CountryRepository : BaseRepository<Country, ModelDbContext>
    {
        public CountryRepository(ModelDbContext context) : base(context)
        {
        }

        public override void OnAdd(Country entity, IUserContext userContext)
        {
            entity.CreationDate = DateTime.Now;
            entity.CreationIp = userContext.IP;
            entity.CreationUser = null;
            OnUpdate(entity, userContext);
        }

        public override void OnUpdate(Country entity, IUserContext userContext)
        {
            entity.EditionDate = DateTime.Now;
            entity.EditionIp = userContext.IP;
            entity.EditionUser = null;
        }
    }
}
