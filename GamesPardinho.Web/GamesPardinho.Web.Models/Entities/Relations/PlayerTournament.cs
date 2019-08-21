using GamesPardinho.Web.Models.Entities.League;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GamesPardinho.Web.Models.Entities.Relations
{
    [Table("League_PlayerTournament")]
    public class PlayerTournament
    {
        public virtual int PlayerId { get; set; }
        public virtual int TournamentId { get; set; }
        public virtual LeagueAccount Player { get; set; }
        public virtual LeagueTournament Tournament { get; set; }
    }
}
