// <copyright file="IGenericRepository.cs" company="TC Sağlık Bakanlığı">
// Copyright(c) 2021 Tüm Hakları Saklıdır
// </copyright>
// <author>İsmail KAŞAN</author>
// <date>1.07.2021</date>

using DomainExample;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace DataExample
{
    /// <summary>
    /// Defines the <see cref="IGenericRepository{T}" />.
    /// </summary>
    /// <typeparam name="T">.</typeparam>
    public interface IGenericRepository<T> where T : class, IEntity
    {
        /// <summary>
        /// The Save.
        /// </summary>
        /// <param name="entity">The entity<see cref="T"/>.</param>
        void Save(T entity);

        /// <summary>
        /// The Get.
        /// </summary>
        /// <param name="id">The id<see cref="long"/>.</param>
        /// <returns>The <see cref="T"/>.</returns>
        T Get(long id);

        /// <summary>
        /// The Update.
        /// </summary>
        /// <param name="entity">The entity<see cref="T"/>.</param>
        void Update(T entity);

        /// <summary>
        /// The Delete.
        /// </summary>
        /// <param name="id">The id<see cref="long"/>.</param>
        void Delete(long id);

        /// <summary>
        /// The All.
        /// </summary>
        /// <returns>The <see cref="IQueryable{T}"/>.</returns>
        IQueryable<T> All();

        /// <summary>
        /// The Find.
        /// </summary>
        /// <param name="predicate">The predicate<see cref="Expression{Func{T, bool}}"/>.</param>
        /// <returns>The <see cref="IQueryable{T}"/>.</returns>
        IQueryable<T> Find(Expression<Func<T, bool>> predicate);
    }
}
