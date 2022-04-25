using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.Entities;
using Core.pagination;

namespace Core.Interfaces.IRepository
{
    public interface IProductRepository:IRepositoryBase<Product>
    {
        IQueryable<ProductBrand> getProductBrandId(Expression<Func<ProductBrand,bool>> expression,bool trackChanges); 
        IQueryable<ProductBrand> getProductBrands(ProductParameters productParameters,bool trackChanges);
        IQueryable<ProductType> getProductTypes(bool trackChanges);   
        IQueryable<ProductType> getProductTypeId(Expression<Func<ProductType,bool>> expression,bool trackChanges); 
        Task<PagedList<Product>> ProductFilterPrice(ProductParameters productParameters);
        Task<PagedList<Product>> ProductSearch(ProductParameters productParameters);
        
    }
}