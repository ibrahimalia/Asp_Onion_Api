using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Data.SeedData
{
    public abstract class BaseFactory<T> where T:class
    {
        protected int _count=1;
        protected readonly StoreContext _storeContext;
        protected BaseFactory(StoreContext storeContext)
        {
            _storeContext = storeContext;
        }
        public BaseFactory<T> Count(int count){
            _count=count;
            return this;

        }
        public  abstract  Task<T> Make();
        public async Task<int> Create(){
            var models =  new List<T>();

            for (int i = 0; i < _count; i++)
                {
                    models.Add(await Make());
                }

            await _storeContext.Set<T>().AddRangeAsync(models);  
           return await _storeContext.SaveChangesAsync();

        }
    }
}