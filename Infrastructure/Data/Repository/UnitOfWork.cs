using System;
using Core.Interfaces.IRepository;

namespace Infrastructure.Data.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreContext _storeContext;
        private IProductRepository _productRepository;

        public UnitOfWork(StoreContext storeContext)
        {
            _storeContext = storeContext;
        }

        public IProductRepository Product => _productRepository??=new ProductRepository(_storeContext);
        

        public void Dispose()
        {
            _storeContext.Dispose();
            GC.SuppressFinalize(this);
        }

        public void Save()
        {
            _storeContext.SaveChanges();
        }
    }
}