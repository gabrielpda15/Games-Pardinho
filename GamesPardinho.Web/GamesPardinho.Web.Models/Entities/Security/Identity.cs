using GamesPardinho.Web.Models.Entities.League;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GamesPardinho.Web.Models.Entities.Security
{
    [Table("Security_IdentityUser")]
    public class Identity : IdentityUser<int>
    {
        [Key]
        public override int Id { get; set; }

        [DataType("varchar")]
        [StringLength(20, ErrorMessage = "Nome deve ter no máximo 20 caracteres")]
        [Display(Name = "Nome")]
        public string FirstName { get; set; }

        [DataType("varchar")]
        [StringLength(80, ErrorMessage = "Sobrenome deve ter no máximo 80 caracteres")]
        [Display(Name = "Sobrenome")]
        public string LastName { get; set; }

        [Display(Name = "Nome do Usuário")]
        [Required(ErrorMessage = "Nome do Usuário é um campo obrigatório")]
        [StringLength(50, ErrorMessage = "Nome do usuário deve ter no máximo 50 caracteres")]
        public override string UserName { get; set; }

        [NotMapped]
        [Display(Name = "Senha")]
        public string Password { get; set; }

        [Required(ErrorMessage = "E-Mail é um campo obrigatório")]
        [Display(Name = "E-Mail")]
        public override string Email { get; set; }

        public virtual int? LeagueAccountId { get; set; }
        public virtual LeagueAccount LeagueAccount { get; set; }
    }
}
