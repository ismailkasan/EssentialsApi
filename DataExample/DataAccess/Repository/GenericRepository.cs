// <copyright file="GenericRepository.cs" company="TC Sağlık Bakanlığı">
// Copyright(c) 2021 Tüm Hakları Saklıdır
// </copyright>
// <author>İsmail KAŞAN</author>
// <date>1.07.2021</date>

using DomainExample;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace DataExample
{
    /// <summary>
    /// Defines the <see cref="GenericRepository{T}" />.
    /// </summary>
    /// <typeparam name="T">.</typeparam>
    public abstract class GenericRepository<T> : IGenericRepository<T> where T : class, IEntity
    {
        /// <summary>
        /// Defines the _dbContext.
        /// </summary>
        private readonly ExampleDbContext _dbContext;

        /// <summary>
        /// Defines the _dbSet.
        /// </summary>
        private readonly DbSet<T> _dbSet;

        /// <summary>
        /// Initializes a new instance of the <see cref="GenericRepository{T}"/> class.
        /// </summary>
        /// <param name="dbContext">The dbContext<see cref="ExampleDbContext"/>.</param>
        protected GenericRepository(ExampleDbContext dbContext)
        {
            this._dbContext = dbContext;
            this._dbSet = _dbContext.Set<T>();
        }

        /// <summary>
        /// The Save.
        /// </summary>
        /// <param name="entity">The entity<see cref="T"/>.</param>
        public void Save(T entity)
        {
            _dbSet.Add(entity);
            _dbContext.SaveChanges();
        }

        /// <summary>
        /// The Get.
        /// </summary>
        /// <param name="id">The id<see cref="long"/>.</param>
        /// <returns>The <see cref="T"/>.</returns>
        public T Get(long id)
        {
            return _dbSet.Find(id);
        }

        /// <summary>
        /// The Update.
        /// </summary>
        /// <param name="entity">The entity<see cref="T"/>.</param>
        public void Update(T entity)
        {
            _dbSet.Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
        }

        /// <summary>
        /// The Delete.
        /// </summary>
        /// <param name="id">The id<see cref="long"/>.</param>
        public void Delete(long id)
        {
            var entity = Get(id);
            _dbSet.Remove(entity);
        }

        /// <summary>
        /// The All.
        /// </summary>
        /// <returns>The <see cref="IQueryable{T}"/>.</returns>
        public IQueryable<T> All()
        {
            return _dbSet.AsNoTracking();
        }

        /// <summary>
        /// The Find.
        /// </summary>
        /// <param name="predicate">The predicate<see cref="Expression{Func{T, bool}}"/>.</param>
        /// <returns>The <see cref="IQueryable{T}"/>.</returns>
        public IQueryable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.Where(predicate);
        }
    }
}
