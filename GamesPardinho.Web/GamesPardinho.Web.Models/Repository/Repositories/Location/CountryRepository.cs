using GamesPardinho.Web.Models.Entities.Location;
using GamesPardinho.Web.Models.Repository.Base;
using GamesPardinho.Web.Models.Repository.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace GamesPardinho.Web.Models.Repository.Repositories.Location
{
    [Repository(typeof(Country))]
    public class CountryRepository : BaseRepository<Country, ModelDbContext>
    {
        public CountryRepository(ModelDbContext context) : base(context)
        {
        }
    }
}
