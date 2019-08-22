using GamesPardinho.Web.Models.Entities.Base;
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

        Task CommitAsync(CancellationToken ct = default);
    }
}
