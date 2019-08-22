using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GamesPardinho.Web.Models.Repository.Base
{
    public interface IRepository<TEntity> where TEntity : Entities.Base.BaseEntity
    {
        Task<IEnumerable<TEntity>> QueryAsync(IUserContext userContext, Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IQueryable<TEntity>> orderBy = null, CancellationToken ct = default);
        Task<TEntity> QueryScalarAsync(IUserContext userContext, Expression<Func<TEntity, bool>> filter = null, CancellationToken ct = default);
        Task<TEntity> QueryByIdAsync(int id, IUserContext userContext, CancellationToken ct = default);
        Task<TEntity> AddAsync(IUserContext userContext, CancellationToken ct = default);
    }
}
