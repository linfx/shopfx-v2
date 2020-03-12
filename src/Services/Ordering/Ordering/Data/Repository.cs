using LinFx.Data;
using LinFx.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Ordering.Data
{
    /// <summary>
    /// 仓储
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public class Repository<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : class, IEntity<TKey>
    {
        public Repository(OrderingContext context)
        {
            Context = context;
            DbSet = Context.Set<TEntity>();
        }

        protected OrderingContext Context { get; }

        protected DbSet<TEntity> DbSet { get; }

        public void Add(TEntity entity)
        {
            if (entity != null)
                DbSet.Add(entity);
        }

        public void AddRange(IList<TEntity> entities)
        {
            if (entities != null && entities.Count > 0)
            {
                DbSet.AddRange(entities);
            }
        }

        public void Remove(TEntity entity)
        {
            if (entity != null)
                DbSet.Remove(entity);
        }

        public IDbContextTransaction BeginTransaction()
        {
            return Context.Database.BeginTransaction();
        }

        public Task<int> SaveChangesAsync()
        {
            return Context.SaveChangesAsync();
        }

        public Task<TEntity> FirstOrDefaultAsync(TKey id)
        {
            return DbSet.FirstOrDefaultAsync(c => c.Id.Equals(id));
        }

        public IQueryable<TEntity> Query()
        {
            return DbSet;
        }

        public IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> predicate)
        {
            return DbSet.Where(predicate);
        }
    }
}