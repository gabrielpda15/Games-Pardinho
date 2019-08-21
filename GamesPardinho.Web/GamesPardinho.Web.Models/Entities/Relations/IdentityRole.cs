using GamesPardinho.Web.Models.Entities.Security;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GamesPardinho.Web.Models.Entities.Relations
{
    [Table("Security_UserRole")]
    public class IdentityRole : IdentityUserRole<int>
    {
        public override int UserId { get; set; }
        public override int RoleId { get; set; }
        public virtual Identity User { get; set; }
        public virtual Role Role { get; set; }
    }
}
