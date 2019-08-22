using GamesPardinho.Web.Models.Entities.Base;
using GamesPardinho.Web.Models.Repository.Base;
using GamesPardinho.Web.Models.Repository.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GamesPardinho.Web.Models.Repository
{
    public sealed class UnitOfWork : IUnitOfWork
    {
        private ModelDbContext Context { get; }

        private IDictionary<Type, object> Repositories { get; }

        public UnitOfWork(ModelDbContext context)
        {
            Context = context;
            Repositories = new Dictionary<Type, object>();

            LoadRepos("GamesPardinho.Web.Models.Repository.Repositories");
        }

        public IAsyncRepository<TEntity> GetRepository<TEntity>() where TEntity : class, IBaseEntity
        {
            return (IAsyncRepository<TEntity>)Repositories[typeof(TEntity)];
        }

        private void LoadRepos(string source)
        {
            var assembly = Assembly.GetAssembly(this.GetType());
            var repos = assembly.GetTypes().Where(x => x.GetCustomAttribute<RepositoryAttribute>() != null).ToArray();
            
            foreach (var repo in repos)
            {
                var @base = repo.GetCustomAttribute<RepositoryAttribute>().Type;
                Repositories.Add(@base, Activator.CreateInstance(repo, Context));
            }            
        }

        public async Task CommitAsync(CancellationToken ct = default)
        {
            await Context.SaveChangesAsync(ct);
        }
    }
}
