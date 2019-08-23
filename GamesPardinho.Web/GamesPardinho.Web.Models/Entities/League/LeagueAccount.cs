using GamesPardinho.Web.Models.Entities.Security;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GamesPardinho.Web.Models.Entities.League
{
    [Table("League_Account")]
    public class LeagueAccount : Base.BaseEntity
    {
        [DataType("varchar")]
        [StringLength(16, ErrorMessage = "O Nome de Invocador pode ter no máximo 16 caracteres.")]    
        [Required(ErrorMessage = "Nome de Invocador é obrigatório")]
        [Display(Name = "Nome de Invocador")]
        [ScaffoldColumn(false)]
        public string SummonerName { get; set; }

        [DataType("varchar")]
        [StringLength(120, ErrorMessage = "O Id de Invocador pode ter no máximo 120 caracteres.")]
        [Required(ErrorMessage = "Id de Invocador é obrigatório")]
        [Display(Name = "Id de Invocador")]
        [ScaffoldColumn(false)]
        public string SummonerId { get; set; }

        [Required(ErrorMessage = "Elo é obrigatório")]
        [ScaffoldColumn(false)]
        public Base.Elo Elo { get; set; }

        [Required(ErrorMessage = "Role é obrigatório")]
        [ScaffoldColumn(false)]
        public Base.Role Role { get; set; }

        public virtual Identity Identity { get; set; }
    }
}
