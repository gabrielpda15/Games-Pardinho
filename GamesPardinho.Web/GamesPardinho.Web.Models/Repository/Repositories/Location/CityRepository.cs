using GamesPardinho.Web.Models.Entities.Location;
using GamesPardinho.Web.Models.Repository.Base;
using GamesPardinho.Web.Models.Repository.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace GamesPardinho.Web.Models.Repository.Repositories.Location
{
    [Repository(typeof(City))]
    public class CityRepository : BaseRepository<City, ModelDbContext>
    {
        public CityRepository(ModelDbContext context) : base(context)
        {
        }
    }
}
