using System;
using System.Collections.Generic;
using System.Text;

namespace GamesPardinho.Web.Models.Entities.Security
{
    public class AuthConfig
    {
        public string SecretKey { get; set; }
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public int ExpirationSeconds { get; set; }
    }
}
