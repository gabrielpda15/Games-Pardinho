using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;

namespace GamesPardinho.Web.Models.Repository.Base
{
    public sealed class UserContext : IUserContext
    {
        public UserContext()
        {
            this.Parametros = new Dictionary<string, object>();
            this.Roles = new List<string>();
            this.Languages = new string[0];
            this.SelectedRoles = new List<string>();
            this.Claims = new List<Claim>();
        }

        public IPrincipal Principal { get; set; }
        public string IP { get; set; }
        public string HostName { get; set; }
        public string[] Languages { get; set; }
        public IEnumerable<string> Roles { get; set; }
        public string SelectedRole { get; set; }
        public IEnumerable<string> SelectedRoles { get; set; }
        public IDictionary<string, object> Parametros { get; set; }
        public IEnumerable<Claim> Claims { get; set; }
    }
}
