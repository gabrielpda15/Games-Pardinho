using GamesPardinho.Web.Models.Entities.Security;
using GamesPardinho.Web.Models.Repository;
using GamesPardinho.Web.Models.Repository.Base;
using GamesPardinho.Web.Models.Repository.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GamesPardinho.Web.Models.Repository.Repositories.Security
{
    [Repository(typeof(Identity))]
    public class IdentityRepository : BaseRepository<Identity, ModelDbContext>
    {
        public IdentityRepository(ModelDbContext context) : base(context)
        {

        }
    }
}
