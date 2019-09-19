﻿using GamesPardinho.Web.Models.Entities.Base;
using GamesPardinho.Web.Models.Repository.Context;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GamesPardinho.Web.Models.Repository.Base
{
    public interface IUnitOfWork
    {
        IAsyncRepository<TEntity> GetRepository<TEntity>() where TEntity : class, IBaseEntity;
        TRepository GetRepository<TEntity, TRepository>() where TEntity : class, IBaseEntity where TRepository : BaseRepository<TEntity, ModelDbContext>;

        Task CommitAsync(CancellationToken ct = default);
    }
}
