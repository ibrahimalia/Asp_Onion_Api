using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Core.Interfaces.IRepository
{
    public interface IRepositoryBase<T> where T:class
    {
        IQueryable<T> FindAll(bool trackChanges);
        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges);
        Task<T> FindByConditionD(Expression<Func<T, bool>> expression, bool trackChanges);
        int get(int a);
        IQueryable<T> Sort(IQueryable<T>items,Expression<Func<T, bool>> expression,string orderType);
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}