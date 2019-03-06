using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Xyzies.SSO.Identity.Data.Entity.Relationship
{
    /// <summary>
    /// Represents a relationship of many to many
    /// </summary>
    /// <typeparam name="TKey">Type of clustered primary key</typeparam>
    /// <typeparam name="TEntity1">Type of 'left' relation entity</typeparam>
    /// <typeparam name="TEntity2">Type of 'right' relation entity</typeparam>
    public abstract class ManyToMany<TKey, TEntity1, TEntity2>
        where TEntity1 : class, IEntity<TKey>
        where TEntity2 : class, IEntity<TKey>
        where TKey : IEquatable<TKey>, IComparable<TKey>, IComparable
    {
        /// <summary>
        /// Id of first entity (table)
        /// </summary>
        [Key]
        public virtual TKey Relation1Id { get; set; }

        /// <summary>
        /// Navigation property of first entity
        /// NOTE: You can rename this by override
        /// </summary>
        [ForeignKey(nameof(Relation1Id))]
        public virtual TEntity1 Entity1 { get; set; }

        /// <summary>
        /// Id of second entity (table)
        /// </summary>
        [Key]
        public virtual TKey Relation2Id { get; set; }

        /// <summary>
        /// Navigation property of second entity
        /// NOTE: You can rename this by override
        /// </summary>
        [ForeignKey(nameof(Relation2Id))]
        public virtual TEntity2 Entity2 { get; set; }
    }
}
