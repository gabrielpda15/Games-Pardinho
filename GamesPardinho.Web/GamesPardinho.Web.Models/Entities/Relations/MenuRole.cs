using GamesPardinho.Web.Models.Entities.Security;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GamesPardinho.Web.Models.Entities.Relations
{
    [Table("Security_MenuRole")]
    public class MenuRole
    {
        public virtual int RoleId { get; set; }
        public virtual int MenuId { get; set; }
        public virtual Role Role { get; set; }
        public virtual Menu Menu { get; set; }
    }
}
