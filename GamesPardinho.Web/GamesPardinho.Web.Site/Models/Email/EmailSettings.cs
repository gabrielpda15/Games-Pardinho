using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GamesPardinho.Web.Site.Models.Email
{
    public class EmailSettings
    {
        public string PrimaryDomain { get; set; }
        public int PrimaryPort { get; set; }
        public string UsernameName { get; set; }
        public string UsernameEmail { get; set; }
        public string UsernamePassword { get; set; }
        public string FromEmail { get; set; }
    }
}
