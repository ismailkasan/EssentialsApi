using Essentials.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Essentials.Data
{
    public abstract class GenericRepository<T> : IGenericRepository<T> where T : class, IEntity
    {
        private readonly EssentialsDbContext _dbContext;
        private readonly DbSet<T> _dbSet;
        protected GenericRepository(EssentialsDbContext dbContext)
        {
            this._dbContext = dbContext;
            this._dbSet = _dbContext.Set<T>();
        }

        public void Save(T entity)
        {
            _dbSet.Add(entity);
            _dbContext.SaveChanges();
        }

        public T Get(long id)
        {
            return _dbSet.Find(id);
        }

        public void Update(T entity)
        {
            _dbSet.Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
        }
               
        public void Delete(long id)
        {
            var entity = Get(id);
            _dbSet.Remove(entity);
        }
              
        public IQueryable<T> All()
        {
            return _dbSet.AsNoTracking();
        }
              
        public IQueryable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.Where(predicate);
        }
    }
}
