using GamesPardinho.Web.Models.Entities.Security;
using GamesPardinho.Web.Models.Repository.Base;
using GamesPardinho.Web.Models.Repository.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace GamesPardinho.Web.Models.Repository.Repositories.Security
{
    [Repository(typeof(Role))]
    public class RoleRepository : BaseRepository<Role, ModelDbContext>
    {
        public RoleRepository(ModelDbContext context) : base(context)
        {
        }
    }
}
