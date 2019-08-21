using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GamesPardinho.Web.Models.Entities.Security
{
    [Table("Security_Role")]
    public class Role : IdentityRole<int>
    {
        [Key]
        public override int Id { get; set; }

        [ScaffoldColumn(false)]
        [ConcurrencyCheck]
        [Display(Name = "Data de Edição")]
        public DateTime? EditionDate { get; set; }

        [ScaffoldColumn(false)]
        public virtual IList<Relations.MenuRole> MenuRoles { get; set; }

        [ScaffoldColumn(false)]
        public virtual ICollection<Relations.IdentityRole> Identities { get; set; }
    }
}
