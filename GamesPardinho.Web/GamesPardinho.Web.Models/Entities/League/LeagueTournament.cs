using GamesPardinho.Web.Models.Entities.Relations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GamesPardinho.Web.Models.Entities.League
{
    [Table("League_Tournament")]
    public class LeagueTournament : Base.BaseDescriptionEntity
    {
        [Required(ErrorMessage = "Nome do torneio é obrigatório")]
        [StringLength(60, ErrorMessage = "Nome do torneio pode ter no maximo 60 caracteres")]
        [DataType("varchar")]
        [ScaffoldColumn(false)]
        public string Name { get; set; }

        public virtual IList<PlayerTournament> Players { get; set; }

        public virtual IList<LeagueTeam> Teams { get; set; }
    }
}
