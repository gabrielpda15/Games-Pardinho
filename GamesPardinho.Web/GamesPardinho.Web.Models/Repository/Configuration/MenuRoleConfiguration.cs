using GamesPardinho.Web.Models.Entities.Relations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace GamesPardinho.Web.Models.Repository.Configuration
{
    public class MenuRoleConfiguration : IEntityTypeConfiguration<MenuRole>
    {
        public void Configure(EntityTypeBuilder<MenuRole> builder)
        {
            builder.HasKey(k => new { k.MenuId, k.RoleId });

            builder.HasOne(x => x.Role)
                .WithMany(x => x.MenuRoles)
                .HasForeignKey(x => x.RoleId);

            builder.HasOne(x => x.Menu)
                .WithMany()
                .HasForeignKey(x => x.MenuId);
        }
    }
}
