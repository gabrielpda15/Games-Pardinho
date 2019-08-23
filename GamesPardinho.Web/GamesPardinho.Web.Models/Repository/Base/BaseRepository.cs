using GamesPardinho.Web.Models.Entities.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GamesPardinho.Web.Models.Repository.Base
{
    public abstract class BaseRepository<TEntity, TContext> : IAsyncRepository<TEntity> 
        where TEntity : class, IBaseEntity
        where TContext : DbContext
    {
        protected DbSet<TEntity> Entities { get; }
        protected TContext Context { get; }

        protected BaseRepository(TContext context)
        {
            Entities = context.Set<TEntity>();
            Context = context;
        }

        public virtual async Task<IEnumerable<TEntity>> QueryAsync(Expression<Func<IQueryable<TEntity>, IQueryable<TEntity>>> filter, IUserContext userContext, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, CancellationToken ct = default)
        {
            return await Task.Run(() =>
            {
                var query = (IQueryable<TEntity>)Entities;

                if (filter == null) filter = x => x;

                query = filter.Compile()(query);

                if (orderBy != null) query = orderBy(query);
                else query = query.OrderBy(x => x.Id);

                return query.ToArray();
            }, ct);
        }

        public virtual async Task<IEnumerable<TEntity>> QueryAsync(Expression<Func<TEntity, bool>> filter, IUserContext userContext, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, CancellationToken ct = default)
        {
            return await Task.Run(() =>
            {
                var query = (IQueryable<TEntity>)Entities;

                if (filter == null) filter = x => true;
                query = query.Where(filter);                

                if (orderBy != null) query = orderBy(query);
                else query = query.OrderBy(x => x.Id);

                return query.ToArray();
            }, ct);            
        }

        public virtual async Task<TEntity> QueryScalarAsync(Expression<Func<TEntity, bool>> filter, IUserContext userContext, CancellationToken ct = default)
        {
            return await Task.Run(async () =>
            {
                var query = (IQueryable<TEntity>)Entities;

                query = query.Where(filter);

                try
                {
                    return await query.SingleAsync(ct);
                }
                catch { throw new QueryException(); }
            }, ct);
        }

        public virtual async Task<TEntity> QueryByIdAsync(int id, IUserContext userContext, CancellationToken ct = default)
        {
            try
            {
                return await ((IQueryable<TEntity>)Entities).SingleAsync(x => x.Id == id, ct);
            }
            catch { throw new QueryException(); }            
        }

        public virtual async Task<TEntity> AddAsync(TEntity entity, IUserContext userContext, CancellationToken ct = default)
        {
            return await Task.Run(async () =>
            {
                entity.CreationDate = DateTime.Now;
                entity.EditionDate = DateTime.Now;
                entity.CreationIp = userContext.IP;
                entity.EditionIp = userContext.IP;
                entity.CreationUser = userContext.Principal.Identity.Name;
                entity.EditionUser = userContext.Principal.Identity.Name;

                await Entities.AddAsync(entity, ct).ConfigureAwait(false);

                return entity;
            }, ct);
        }

        public virtual async Task<IEnumerable<TEntity>> AddAllAsync(IEnumerable<TEntity> entities, IUserContext userContext, CancellationToken ct = default)
        {
            return await Task.Run(async () =>
            {
                foreach (var entity in entities)
                {
                    entity.CreationDate = DateTime.Now;
                    entity.EditionDate = DateTime.Now;
                    entity.CreationIp = userContext.IP;
                    entity.EditionIp = userContext.IP;
                    entity.CreationUser = userContext.Principal.Identity.Name;
                    entity.EditionUser = userContext.Principal.Identity.Name;
                }

                await Entities.AddRangeAsync(entities, ct).ConfigureAwait(false);

                return entities;
            }, ct);            
        }

        public virtual async Task<TEntity> UpdateAsync(TEntity entity, IUserContext userContext, CancellationToken ct = default)
        {
            return await Task.Run(() =>
            {
                entity.EditionDate = DateTime.Now;
                entity.EditionIp = userContext.IP;
                entity.EditionUser = userContext.Principal.Identity.Name;

                Entities.Update(entity);
                return entity;
            }, ct);
        }

        public virtual async Task<IEnumerable<TEntity>> UpdateAllAsync(IEnumerable<TEntity> entities, IUserContext userContext, CancellationToken ct = default)
        {
            return await Task.Run(() =>
            {
                foreach (var entity in entities)
                {
                    entity.EditionDate = DateTime.Now;
                    entity.EditionIp = userContext.IP;
                    entity.EditionUser = userContext.Principal.Identity.Name;
                }                

                Entities.UpdateRange(entities);

                return entities;
            }, ct);
        }

        public virtual async Task<bool> DeleteAsync(int id, IUserContext userContext, CancellationToken ct = default)
        {
            return await Task.Run(async () =>
            {
                TEntity toRemove;
                try
                {
                    toRemove = await Entities.SingleAsync(x => x.Id == id);                                        
                }
                catch { throw new QueryException(); }

                Entities.Remove(toRemove);

                return true;
            }, ct);
        }

        public virtual async Task<string> DeleteAllAsync(string ids, IUserContext userContext, CancellationToken ct = default)
        {
            return await Task.Run(async () =>
            {
                var toReturn = new List<string>();

                foreach (var sid in ids.Split(','))
                {
                    try
                    {
                        if (!int.TryParse(sid, out int id)) throw new FormatException("Can't convert given ids to integer");
                        var toRemove = await Entities.SingleAsync(x => x.Id == id, ct);
                        Entities.Remove(toRemove);
                    }
                    catch (FormatException ex) { throw ex; }
                    catch { toReturn.Add(sid); }
                }

                if (toReturn.Count == 0) return null;
                return string.Join(',', toReturn);
            }, ct);
        }
    }
}
