using GamesPardinho.Web.Models.Entities.Security;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GamesPardinho.Web.Site
{
    public static class ApiManager
    {
        public static HttpClient Client { get; }
        public const string API_ENDPOINT = "http://localhost/GamesPardinho/api/";

        public static TokenData token { get; set; } = null;
        private const string MEDIA_TYPE = "application/json";

        private class CheckObject
        {
            public string Result { get; set; }

            public bool Check()
            {
                return Result == "Ok!";
            }
        }

        public class ConnectionObject
        {
            public string Color { get; set; }
            public string Text { get; set; }
            public bool Connected { get; set; }

            public static readonly ConnectionObject Online = new ConnectionObject { Connected = true, Color = "#00c71e", Text = "Online" };
            public static readonly ConnectionObject Offline = new ConnectionObject { Connected = true, Color = "#c70017", Text = "Offline" };
        }

        public static ConnectionObject CheckConnection()
        {
            return Test().GetAwaiter().GetResult() ? ConnectionObject.Online : ConnectionObject.Offline;
        }

        public static async Task<bool> Test()
        {
            try
            {
                var obj = await Get<CheckObject>("Test", anonymous: true).ConfigureAwait(false);

                return obj.Check();
            }
            catch { return false; }            
        }

        public static void Validate(this TokenData token)
        {
            var validation = token.TryValidate();
            if (!validation)
                throw new TokenExpiratedException();
        }

        public static bool TryValidate(this TokenData token)
        {
            return token == null ? false : (token.Expiration > DateTime.Now);            
        }

        public static JwtSecurityToken Read(this TokenData token)
        {
            var handler = new JwtSecurityTokenHandler();
            return handler.ReadToken(token.ToString()) as JwtSecurityToken;
        }

        public class TokenData
        {
            public string Token { get; set; }
            public DateTime Creation { get; set; }
            public DateTime Expiration { get; set; }

            public override string ToString()
            {
                return Token;
            }
        }

        public class AuthorizationException : Exception
        {
            public AuthorizationException(string message) : base(message) { }
        }

        public class TokenExpiratedException : Exception
        {
            public TokenExpiratedException() : base("Token expirado!") { }
        }

        static ApiManager()
        {
            Client = new HttpClient
            {
                BaseAddress = new Uri(API_ENDPOINT)
            };
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MEDIA_TYPE));
        }

        private static void ClearAuthorization()
        {
            Client.DefaultRequestHeaders.Clear();
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MEDIA_TYPE));
            token = null;
        }

        public static void Logout()
        {
            ClearAuthorization();
        }

        public static async Task Login(User user, CancellationToken ct = default)
        {
            var content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, MEDIA_TYPE);
            content.Headers.ContentType = new MediaTypeHeaderValue(MEDIA_TYPE);

            var response = await Client.PostAsync("login", content, ct);

            if (!response.IsSuccessStatusCode)
            {
                throw new AuthorizationException("Login inválido!");
            }

            var json = await response.Content.ReadAsStringAsync();
            token = JsonConvert.DeserializeObject<TokenData>(json);

            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.ToString());
        }

        public static async Task<bool?> CheckRole(string role, CancellationToken ct = default)
        {
            if (!token.TryValidate()) return null;

            var response = await Client.PostAsync($"login/Validate/{role}", new StringContent(string.Empty), ct);

            if (response.StatusCode == System.Net.HttpStatusCode.Accepted) return true;
            else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest) return false;
            else return null;
        }

        public static async Task<T> Get<T>(string url, CancellationToken ct = default, bool anonymous = false)
        {
            if (!anonymous)
                token.Validate();                

            var response = await Client.GetAsync(url, ct);
            var responseBody = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
                throw new HttpRequestException(responseBody);

            var obj = JsonConvert.DeserializeObject<T>(responseBody);
            return obj;
        }

        public static async Task<T> Post<T, TBody>(string url, TBody body, CancellationToken ct = default)
        {
            token.Validate();

            var content = new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, MEDIA_TYPE);
            content.Headers.ContentType = new MediaTypeHeaderValue(MEDIA_TYPE);

            var response = await Client.PostAsync(url, content, ct);
            var responseBody = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
                throw new HttpRequestException(responseBody);

            var obj = JsonConvert.DeserializeObject<T>(responseBody);
            return obj;
        }

        public static async Task Post(string url, CancellationToken ct = default)
        {
            token.Validate();

            await Post<object, object>(url, null, ct);
        }


    }
}
