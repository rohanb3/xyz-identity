using System;
using System.ComponentModel.DataAnnotations;

namespace Xyzies.SSO.Identity.Data.Entity
{
    /// <summary>
    /// A base entity
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public abstract class BaseEntity<TKey> : IEntity<TKey>
        where TKey : IEquatable<TKey>, IComparable<TKey>, IComparable
    {
        /// <inheritdoc />
        [Key]
        public virtual TKey Id { get; set; }

        /// <inheritdoc />
        public BaseEntity()
        {
            this.Id = default(TKey);
        }

        /// <inheritdoc />
        public bool EqualsByKey(TKey key)
        {
            return this.Id.Equals(key);
        }

        /// <summary>
        /// Create a new instance of an entity
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns>A new instance of entity</returns>
        public static TEntity Create<TEntity>()
            where TEntity : IEntity<TKey>
        {
            TEntity instance = Activator.CreateInstance<TEntity>();
            instance.Id = default(TKey);

            return instance;
        }
    }
}
