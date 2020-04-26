using LinFx.Domain.Models;

namespace Ordering.Data
{
    /// <summary>
    /// 泛型仓储
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public class Repository<TEntity, TKey> : LinFx.Data.Repository<TEntity, TKey> where TEntity : class, IEntity<TKey>
    {
        public Repository(OrderingContext context) : base(context)
        {
        }
    }

    /// <summary>
    /// 泛型仓储
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class Repository<TEntity> : LinFx.Data.Repository<TEntity, long> where TEntity : class, IEntity<long>
    {
        public Repository(OrderingContext context) : base(context)
        {
        }
    }
}