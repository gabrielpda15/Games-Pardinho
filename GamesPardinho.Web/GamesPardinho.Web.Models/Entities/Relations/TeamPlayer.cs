using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GamesPardinho.Web.Models.Entities.Relations
{
    [Table("League_TeamPlayer")]
    public class TeamPlayer
    {
        public virtual int TeamId { get; set; }
        public virtual int PlayerId { get; set; }
        public virtual League.LeagueAccount Player { get; set; }
        public virtual League.LeagueTeam Team { get; set; }
    }
}
