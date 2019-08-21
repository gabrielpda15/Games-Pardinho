using GamesPardinho.Web.Models.Entities.Relations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GamesPardinho.Web.Models.Entities.League
{
    [Table("League_Team")]
    public class LeagueTeam : Base.BaseEntity
    {
        [Required(ErrorMessage = "Nome do time é obrigatório")]
        [StringLength(60, ErrorMessage = "Nome do time pode ter no maximo 60 caracteres")]
        [Display(Name = "Nome do Time")]
        [DataType("varchar")]
        [ScaffoldColumn(false)]
        public string Name { get; set; }

        [Display(Name = "Pontos de Elo")]
        [ScaffoldColumn(false)]
        public int EloPoint { get; set; }

        [Display(Name = "Tentativas")]
        [ScaffoldColumn(false)]
        public int Attempts { get; set; }

        public virtual IList<TeamPlayer> Players { get; set; }

        public virtual LeagueTournament Tournament { get; set; }
    }
}
