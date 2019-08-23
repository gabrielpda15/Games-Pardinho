using GamesPardinho.Web.Models.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GamesPardinho.Web.Models.Repository.Base
{
    public interface IAsyncRepository<TEntity> where TEntity : class, IBaseEntity
    {
        void OnAdd(TEntity entity, IUserContext userContext);
        void OnUpdate(TEntity entity, IUserContext userContext);
        Task<IEnumerable<TEntity>> QueryAsync(Expression<Func<IQueryable<TEntity>, IQueryable<TEntity>>> filter, IUserContext userContext, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, CancellationToken ct = default);
        Task<IEnumerable<TEntity>> QueryAsync(Expression<Func<TEntity, bool>> filter, IUserContext userContext, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, CancellationToken ct = default);
        Task<TEntity> QueryScalarAsync(Expression<Func<TEntity, bool>> filter, IUserContext userContext, CancellationToken ct = default);
        Task<TEntity> QueryByIdAsync(int id, IUserContext userContext, CancellationToken ct = default);
        Task<TEntity> AddAsync(TEntity entity, IUserContext userContext, CancellationToken ct = default);
        Task<IEnumerable<TEntity>> AddAllAsync(IEnumerable<TEntity> entities, IUserContext userContext, CancellationToken ct = default);
        Task<TEntity> UpdateAsync(TEntity entity, IUserContext userContext, CancellationToken ct = default);
        Task<IEnumerable<TEntity>> UpdateAllAsync(IEnumerable<TEntity> entities, IUserContext userContext, CancellationToken ct = default);
        Task<bool> DeleteAsync(int id, IUserContext userContext, CancellationToken ct = default);
        Task<string> DeleteAllAsync(string ids, IUserContext userContext, CancellationToken ct = default);
    }
}
