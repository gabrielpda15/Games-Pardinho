using GamesPardinho.Web.Models.Entities.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GamesPardinho.Web.Extensions
{
    public static class EntityFrameworkExtension
    {
        public static async Task<int?> SetIdentityInsertAsync<TEntity>(this DatabaseFacade database, bool value, CancellationToken ct = default) where TEntity : class, IBaseEntity
        {
            var table = typeof(TEntity).GetCustomAttributes(typeof(TableAttribute), true).FirstOrDefault() as TableAttribute;
            var onOff = value ? "ON" : "OFF";

            if (table != null)
            {
                return await database.ExecuteSqlRawAsync($"SET IDENTITY_INSERT [dbo].[{table.Name}] {onOff}", ct);
            }

            return null;
        }

    }
}
