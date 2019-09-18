using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace GamesPardinho.Web.Extensions
{
    public static class HttpExtension
    {
        public async static Task<T> ReadAsObjectAsync<T>(this HttpContent content) where T : class
        {
            var result = await content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(result);
        }
    }
}
