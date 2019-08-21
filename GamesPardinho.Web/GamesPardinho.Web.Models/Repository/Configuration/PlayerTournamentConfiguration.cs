using GamesPardinho.Web.Models.Entities.Relations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace GamesPardinho.Web.Models.Repository.Configuration
{
    public class PlayerTournamentConfiguration : IEntityTypeConfiguration<PlayerTournament>
    {
        public void Configure(EntityTypeBuilder<PlayerTournament> builder)
        {
            builder.HasKey(k => new { k.PlayerId, k.TournamentId });

            builder.HasOne(x => x.Tournament)
                .WithMany(x => x.Players)
                .HasForeignKey(x => x.TournamentId);

            builder.HasOne(x => x.Player)
                .WithMany()
                .HasForeignKey(x => x.PlayerId);
        }
    }
}
