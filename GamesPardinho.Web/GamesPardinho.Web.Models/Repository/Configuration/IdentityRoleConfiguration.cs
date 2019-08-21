using GamesPardinho.Web.Models.Entities.Relations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace GamesPardinho.Web.Models.Repository.Configuration
{
    public class IdentityRoleConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasKey(k => new { k.RoleId, k.UserId });

            builder.HasOne(t => t.Role)
                .WithMany()
                .HasForeignKey(x => x.RoleId);

            builder.HasOne(t => t.User)
                .WithMany()
                .HasForeignKey(x => x.UserId);
        }
    }
}
