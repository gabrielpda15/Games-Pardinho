using GamesPardinho.Web.Models.Entities.Location;
using GamesPardinho.Web.Models.Repository.Base;
using GamesPardinho.Web.Models.Repository.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GamesPardinho.Web.Models.Repository.Repositories.Location
{
    [Repository(typeof(City))]
    public class CityRepository : BaseRepository<City, ModelDbContext>
    {
        public CityRepository(ModelDbContext context) : base(context)
        {
        }

        public override void OnAdd(City entity, IUserContext userContext)
        {
            entity.CreationDate = DateTime.Now;
            entity.CreationIp = userContext.IP;
            entity.CreationUser = null;
            OnUpdate(entity, userContext);
        }

        public override void OnUpdate(City entity, IUserContext userContext)
        {
            entity.EditionDate = DateTime.Now;
            entity.EditionIp = userContext.IP;
            entity.EditionUser = null;
        }
    }
}
