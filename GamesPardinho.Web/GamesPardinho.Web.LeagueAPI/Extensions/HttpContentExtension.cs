using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace GamesPardinho.Web.LeagueAPI.Extensions
{
    public static class HttpContentExtension
    {
        public static async Task<TEntity> ReadAsAsync<TEntity>(this HttpContent content)
        {
            var raw = await content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<TEntity>(raw);
        }
    }
}
