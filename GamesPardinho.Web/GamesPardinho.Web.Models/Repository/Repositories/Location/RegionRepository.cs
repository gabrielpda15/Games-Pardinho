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
    [Repository(typeof(Region))]
    public class RegionRepository : BaseRepository<Region, ModelDbContext>
    {
        public RegionRepository(ModelDbContext context) : base(context)
        {
        }

        public override void OnAdd(Region entity, IUserContext userContext)
        {
            entity.CreationDate = DateTime.Now;
            entity.CreationIp = userContext.IP;
            entity.CreationUser = null;
            OnUpdate(entity, userContext);
        }

        public override void OnUpdate(Region entity, IUserContext userContext)
        {
            entity.EditionDate = DateTime.Now;
            entity.EditionIp = userContext.IP;
            entity.EditionUser = null;
        }
    }
}
