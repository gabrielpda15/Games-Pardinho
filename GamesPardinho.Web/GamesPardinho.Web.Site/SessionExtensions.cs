using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GamesPardinho.Web.Site
{
    public static class SessionExtensions
    {
        public static bool Validate(this ISession session)
        {
            if (!session.IsAvailable) return false;
            if (!session.Keys.Contains("auth-token")) return false;

            var token = JsonConvert.DeserializeObject<ApiManager.TokenData>(session.GetString("auth-token"));
            try
            {
                token.Validate();
                var result = token.Read();
                return true;
            }
            catch (ApiManager.TokenExpiratedException) { }
            return false;
        }

        public static bool TryGetString(this ISession session, string key, out string value)
        {
            value = null;
            if (session.Keys.Contains(key))
            {
                value = session.GetString(key);
                session.Remove(key);
                return true;
            }
            return false;
        }
    }
}
