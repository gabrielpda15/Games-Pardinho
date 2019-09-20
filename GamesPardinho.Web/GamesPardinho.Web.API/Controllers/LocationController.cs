using GamesPardinho.Web.Extensions;
using GamesPardinho.Web.Models.Entities.Location;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using AutoMapper;
using GamesPardinho.Web.Models.Repository.Base;
using GamesPardinho.Web.Models.Repository.Repositories.Location;
using GamesPardinho.Web.Models.Controller;
using Microsoft.EntityFrameworkCore;
using GamesPardinho.Web.Models.Entities.Base;

namespace GamesPardinho.Web.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class LocationController : GController
    {
        protected CountryRepository CountryRepository { get; }
        protected RegionRepository RegionRepository { get; }
        protected CityRepository CityRepository { get; }
        protected IUserContext UserContext { get; }
        protected IUnitOfWork UnitOfWork { get; }

        public LocationController(IUnitOfWork unitOfWork, IUserContext userContext) : base()
        {
            CountryRepository = unitOfWork.GetRepository<Country, CountryRepository>();
            RegionRepository = unitOfWork.GetRepository<Region, RegionRepository>();
            CityRepository = unitOfWork.GetRepository<City, CityRepository>();

            UserContext = userContext;

            UnitOfWork = unitOfWork;
        }

        public class SetValuesObject
        {
            public class RepoObj
            {
                public bool? Added { get; set; } = null;
                public bool? Cleaned { get; set; } = null;
                public int? Count { get; set; } = null;
            }

            public class Status
            {
                public IList<object> Commited { get; set; } = null;
                public Exception Error { get; set; } = null;
            }

            public RepoObj Country { get; set; } = null;
            public RepoObj Region { get; set; } = null;
            public RepoObj City { get; set; } = null;
            public Status UnitOfWork { get; set; } = null;
        }

        [HttpPost("InitialCreate")]
        public async Task<IActionResult> InitialCreate(CancellationToken ct)
        {
            IList<Country> countries = null;
            IList<Region> regions = null;
            IList<City> cities = null;

            try
            {
                countries = JsonConvert.DeserializeObject<IList<Country>>(Properties.Resources.Countries);
                regions = JsonConvert.DeserializeObject<IList<Region>>(Properties.Resources.Regions);
                cities = JsonConvert.DeserializeObject<IList<City>>(Properties.Resources.Cities);
                var test = cities.Where(x => x.Id == 1);
            }
            catch (Exception ex) { return Internal(JsonConvert.SerializeObject(ex)); }

            try
            {
                var obj = new SetValuesObject()
                {
                    Country = null,
                    Region = null,
                    City = null,
                    UnitOfWork = new SetValuesObject.Status { Commited = new List<object>() }
                };

                var storage = new
                {
                    Countries = await CountryRepository.QueryAsync(x => x, UserContext, ct: ct),
                    Regions = await RegionRepository.QueryAsync(x => x, UserContext, ct: ct),
                    Cities = await CityRepository.QueryAsync(x => x, UserContext, ct: ct)
                };

                if (storage.Countries.Count() != countries.Count)
                {
                    if (storage.Countries.Count() > 0)
                        await CountryRepository.DeleteAllAsync(string.Join(",", storage.Countries.Select(x => x.Id.ToString())), UserContext, ct);

                    await CountryRepository.AddAllAsync(countries, UserContext, ct);

                    obj.Country = new SetValuesObject.RepoObj
                    {
                        Added = true,
                        Cleaned = storage.Countries.Count() > 0,
                        Count = countries.Count
                    };

                    await CommitStatus<Country>(obj, ct);

                }
                else
                {
                    obj.Country = new SetValuesObject.RepoObj
                    {
                        Added = false
                    };
                }

                if (storage.Regions.Count() != regions.Count)
                {
                    if (storage.Regions.Count() > 0)
                        await RegionRepository.DeleteAllAsync(string.Join(",", storage.Regions.Select(x => x.Id.ToString())), UserContext, ct);

                    await RegionRepository.AddAllAsync(regions, UserContext, ct);

                    obj.Region = new SetValuesObject.RepoObj
                    {
                        Added = true,
                        Cleaned = storage.Regions.Count() > 0,
                        Count = regions.Count
                    };

                    await CommitStatus<Region>(obj, ct);
                }
                else
                {
                    obj.Region = new SetValuesObject.RepoObj
                    {
                        Added = false
                    };
                }

                if (storage.Cities.Count() != cities.Count)
                {
                    if (storage.Cities.Count() > 0)
                        await CityRepository.DeleteAllAsync(string.Join(",", storage.Cities.Select(x => x.Id.ToString())), UserContext, ct);

                    var test = cities.Where(x => x.Id == 1).ToArray();

                    await CityRepository.AddAllAsync(cities, UserContext, ct);

                    obj.Region = new SetValuesObject.RepoObj
                    {
                        Added = true,
                        Cleaned = storage.Cities.Count() > 0,
                        Count = cities.Count
                    };

                    await CommitStatus<City>(obj, ct);
                }
                else
                {
                    obj.Region = new SetValuesObject.RepoObj
                    {
                        Added = false
                    };
                }

                if (obj.UnitOfWork.Commited.Select(x => (bool)((dynamic)x).Ok).Where(x => x == false).Count() == 0)
                {
                    return CreatedAtAction(nameof(InitialCreate), JsonConvert.SerializeObject(obj));
                }
                else
                {
                    return Internal(JsonConvert.SerializeObject(obj));
                }
            }
            catch (Exception ex) { return Internal(JsonConvert.SerializeObject(ex)); }
        }

        private async Task CommitStatus<TEntity>(SetValuesObject obj, CancellationToken ct) where TEntity : class, IBaseEntity
        {
            await UnitOfWork.ExecuteAsync(async (context, ct) =>
            {
                await context.Database.SetIdentityInsertAsync<TEntity>(true, ct);

                try
                {
                    await UnitOfWork.CommitAsync(ct);

                    obj.UnitOfWork.Commited.Add(new { Repository = typeof(TEntity).Name, Ok = true });
                }
                catch (Exception ex)
                {
                    obj.UnitOfWork.Commited.Add(new { Repository = typeof(TEntity).Name, Ok = false });
                    obj.UnitOfWork.Error = ex;
                }

                await context.Database.SetIdentityInsertAsync<TEntity>(false, ct);
            }, ct);            
        }

        [AllowAnonymous]
        [HttpGet("Countries")]
        [ProducesResponseType(typeof(IEnumerable<Country>), StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetCountries(CancellationToken ct)
        {
            var query = await CountryRepository.QueryAsync(x => x, UserContext, x => x.OrderBy(o => o.Name), ct);
            return Ok(query.Select(x => new { x.Id, x.Code, x.Name }).OrderBy(x => x.Name));
        }

        [AllowAnonymous]
        [HttpGet("Regions/{idCountry}")]
        [ProducesResponseType(typeof(IEnumerable<Region>), StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetRegions([FromRoute]int idCountry, CancellationToken ct)
        {
            var query = await RegionRepository.QueryAsync(x => x.CountryId == idCountry, UserContext, x => x.OrderBy(o => o.Name), ct);
            return Ok(query.Select(x => new { x.Id, x.Name }).OrderBy(x => x.Name));
        }

        [AllowAnonymous]
        [HttpGet("Cities/{idRegion}")]
        [ProducesResponseType(typeof(IEnumerable<City>), StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetCities([FromRoute]int idRegion, CancellationToken ct)
        {
            var query = await CityRepository.QueryAsync(x => x.RegionId == idRegion, UserContext, x => x.OrderBy(o => o.Name), ct);
            return Ok(query.Select(x => new { x.Id, x.Name }).OrderBy(x => x.Name));
        }

        /*
        private const string BASE_ADDRESS = "https://www.geonames.org/";
        private const string REQUEST_FORMAT = "childrenJSON?formatted=true&geonameId={0}&style=full";
        private const int CONTINENTS_ID = 6295630;
        private HttpClient Client { get; }

        public LocationController()
        {
            Client = new HttpClient();
            Client.BaseAddress = new Uri(BASE_ADDRESS);
        }

        private string GetRequestUrl(int id)
        {
            return string.Format(REQUEST_FORMAT, id);
        }

        private async Task<IList<GeoName>> GetWithId(int id, CancellationToken ct = default)
        {
            var response = await Client.GetAsync(GetRequestUrl(id), ct);

            if (response.IsSuccessStatusCode)
            {
                var gno = await response.Content.ReadAsObjectAsync<GeoNameObject>();
                return gno.GeoNames;
            }

            return null;
        }

        private async Task<IList<GeoName>> GetCountriesFromServer(CancellationToken ct = default)
        {
            var continents = await GetWithId(CONTINENTS_ID, ct);
            var countries = new List<GeoName>();

            foreach (var continent in continents)
            {
                var gno = await GetWithId(continent.GeoNameId, ct);

                if (gno != null ? gno.Count > 0 : false)
                    countries.AddRange(gno);
            }

            return countries.Count > 0 ? countries : null;
        }

        private IEnumerable<TOutput> Map<TInput, TOutput>(IEnumerable<TInput> input, Func<TInput, TOutput> relation)
        {
            var result = new List<TOutput>();

            foreach (var item in input)
            {
                result.Add(relation(item));
            }

            return result.Count > 0 ? result : null;
        }

        private IEnumerable<object> Map(IEnumerable<GeoName> gns, string lang)
        {
            return Map(gns, x =>
            {
                var aName = x.AlternateNames.SingleOrDefault(x => x.Lang == lang);
                return new
                {
                    x.GeoNameId,
                    x.AsciiName,
                    x.ToponymName,
                    Name = (aName == null ? x.Name : aName.Name),
                    x.Code,
                    x.TimeZone
                };
            }).OrderBy(x => x.Name);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Get([FromRoute]string lang)
        {
            return await Task.FromResult(Ok(new
            {
                Source = BASE_ADDRESS,
                Usage = BASE_ADDRESS + REQUEST_FORMAT,
                Params = new[] { new { geonameId = "int" } },
                InitialRequest = BASE_ADDRESS + string.Format(REQUEST_FORMAT, CONTINENTS_ID),
                Lang = lang
            }));
        }
        
        [AllowAnonymous]
        [HttpGet("Continents")]
        public async Task<IActionResult> GetContinents([FromRoute]string lang, CancellationToken ct)
        {
            return Ok(Map(await GetWithId(CONTINENTS_ID, ct), lang));
        }

        [AllowAnonymous]
        [HttpGet("Continents/Values")]
        public async Task<IActionResult> GetContinentsValues([FromRoute]string lang, CancellationToken ct)
        {
            return Ok(Map(await GetWithId(CONTINENTS_ID, ct), lang).Select(x => ((dynamic)x).Name));
        }

        [AllowAnonymous]
        [HttpGet("Countries")]
        public async Task<IActionResult> GetCountries([FromRoute]string lang, CancellationToken ct)
        {
            return Ok(Map(await GetCountriesFromServer(ct), lang));
        }

        [AllowAnonymous]
        [HttpGet("Countries/Values")]
        public async Task<IActionResult> GetCountriesValues([FromRoute]string lang, CancellationToken ct)
        {
            return Ok(Map(await GetCountriesFromServer(ct), lang).Select(x => ((dynamic)x).Name));
        }

        [AllowAnonymous]
        [HttpGet("CityState/{id}")]
        public async Task<IActionResult> GetCityState([FromRoute]string lang, [FromRoute]int id, CancellationToken ct)
        {
            return Ok(Map(await GetWithId(id, ct), lang));
        }

        [AllowAnonymous]
        [HttpGet("CityState/{id}/Values")]
        public async Task<IActionResult> GetCityStateValues([FromRoute]string lang, [FromRoute]int id, CancellationToken ct)
        {
            return Ok(Map(await GetWithId(id, ct), lang).Select(x => ((dynamic)x).Name));
        }*/
    }
}