using System;
using System.Threading.Tasks;

namespace Core.Interfaces.IRepository
{
    public interface IUnitOfWork: IDisposable
    {
        IProductRepository Product {get;} 
        void Save();
    }
}