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

namespace GamesPardinho.Web.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]/{lang}")]
    public class LocationController : ControllerBase
    {
        protected CountryRepository CountryRepository { get; }
        protected RegionRepository RegionRepository { get; }
        protected CityRepository CityRepository { get; }
        protected IUserContext UserContext { get; }

        public LocationController(IUnitOfWork unitOfWork, IUserContext userContext) : base()
        {
            CountryRepository = unitOfWork.GetRepository<Country, CountryRepository>();
            RegionRepository = unitOfWork.GetRepository<Region, RegionRepository>();
            CityRepository = unitOfWork.GetRepository<City, CityRepository>();

            UserContext = userContext;
        }

        [AllowAnonymous]
        [HttpGet("Countries")]
        [ProducesResponseType(typeof(IEnumerable<Country>), StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetCountries(CancellationToken ct)
        {
            return Ok(await CountryRepository.QueryAsync(x => x, UserContext, x => x.OrderBy(o => o.Name), ct));
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