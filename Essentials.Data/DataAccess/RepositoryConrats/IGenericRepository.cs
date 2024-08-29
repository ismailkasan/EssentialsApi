using System;
using System.Linq;
using System.Linq.Expressions;
using Essentials.Domain;

namespace Essentials.Data
{
    public interface IGenericRepository<T> where T : class, IEntity
    {
        void Save(T entity);
        T Get(long id);
        void Update(T entity);
        void Delete(long id);
        IQueryable<T> All();
        IQueryable<T> Find(Expression<Func<T, bool>> predicate);
    }
}
