using GamesPardinho.Web.Models.Entities;
using GamesPardinho.Web.Models.Entities.League;
using GamesPardinho.Web.Models.Entities.Relations;
using GamesPardinho.Web.Models.Entities.Security;
using GamesPardinho.Web.Models.Repository.Configuration;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace GamesPardinho.Web.Models.Repository.Context
{
    public class ModelDbContext : IdentityDbContext<Identity, Role, int, IdentityClaim, IdentityRole, IdentityLogin, RoleClaim, IdentityToken>
    {
        public ModelDbContext(DbContextOptions<ModelDbContext> options) : base(options) { }

        // League
        public DbSet<LeagueAccount> LeagueAccounts { get; set; }
        public DbSet<LeagueTeam> LeagueTeams { get; set; }
        public DbSet<LeagueTournament> LeagueTournaments { get; set; }

        // Security
        public DbSet<Menu> Menus { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new MenuRoleConfiguration());
            builder.ApplyConfiguration(new IdentityRoleConfiguration());
            builder.ApplyConfiguration(new TeamPlayerConfiguration());
            builder.ApplyConfiguration(new PlayerTournamentConfiguration());

            builder.Entity<LeagueAccount>().HasOne(x => x.Identity).WithOne(x => x.LeagueAccount).HasForeignKey<Identity>(x => x.LeagueAccountId);
            builder.Entity<LeagueTeam>().HasOne(x => x.Tournament).WithMany(x => x.Teams);


            // Config Tables
            builder.Entity<Identity>().ToTable("Security_IdentityUser");
            builder.Entity<IdentityClaim>().ToTable("Security_UserClaim");
            builder.Entity<IdentityLogin>().ToTable("Security_UserLogin");
            builder.Entity<IdentityRole>().ToTable("Security_UserRole");
            builder.Entity<IdentityToken>().ToTable("Security_UserToken");
            builder.Entity<Role>().ToTable("Security_Role");
            builder.Entity<RoleClaim>().ToTable("Security_RoleClaim");
        }
    }
}
