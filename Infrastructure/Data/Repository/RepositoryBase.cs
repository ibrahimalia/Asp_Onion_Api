using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.Interfaces.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repository
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        private readonly StoreContext _storeContext;
        public RepositoryBase(StoreContext storeContext)
        {
         _storeContext=storeContext;
        }

        public void Create(T entity) =>   
          _storeContext.Set<T>().Add(entity);
        

        public void Delete(T entity)=>
            _storeContext.Set<T>().Remove(entity);

        public IQueryable<T> FindAll(bool trackChanges)=>
            !trackChanges ?_storeContext.Set<T>().AsNoTracking() :_storeContext.Set<T>();

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges)=>
            !trackChanges? _storeContext.Set<T>().Where(expression).AsNoTracking() 
            :
            _storeContext.Set<T>().Where(expression);
        public  async Task<T> FindByConditionD(Expression<Func<T, bool>> expression, bool trackChanges)=>
            !trackChanges? await _storeContext.Set<T>().Where(expression).AsNoTracking().SingleOrDefaultAsync() 
            :
            await _storeContext.Set<T>().Where(expression).SingleOrDefaultAsync();
            
       public int get (int a){
           return a;
       }

        public IQueryable<T> Sort(IQueryable<T> items, Expression<Func<T, bool>> expression, string orderType)
        {
           if (orderType.Trim().ToLower() == "desc")
           {
              return items.OrderByDescending(expression);
           }else
           {
              return items.OrderBy(expression);
               
           }
        }

        public void Update(T entity)=>
           _storeContext.Set<T>().Update(entity);

       
    }
}