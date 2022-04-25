using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces.IRepository;
using Core.pagination;
using Infrastructure.Data.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repository
{
    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        private readonly StoreContext _storContext;

        public ProductRepository(StoreContext storeContext) : base(storeContext)
        {
            _storContext = storeContext;
          
        }

        public IQueryable<ProductBrand> getProductBrandId(
        Expression<Func<ProductBrand, bool>> expression,
        bool trackChanges
        ) =>
            !trackChanges ? _storContext.Set<ProductBrand>().Where(expression).AsNoTracking() :
                          _storContext.Set<ProductBrand>().Where(expression);



        public IQueryable<ProductBrand> getProductBrands(ProductParameters productParameters,bool trackChanges)=>
          !trackChanges ? _storContext.Set<ProductBrand>().AsNoTracking()
          :
          _storContext.Set<ProductBrand>();
       

        public IQueryable<ProductType> getProductTypeId(
            Expression<Func<ProductType, bool>> expression,
             bool trackChanges)=>
                    !trackChanges ? _storContext.Set<ProductType>().Where(expression).AsNoTracking() :
                                _storContext.Set<ProductType>().Where(expression);


        public IQueryable<ProductType> getProductTypes(bool trackChanges)=>
           !trackChanges ? _storContext.Set<ProductType>().AsNoTracking():
                        _storContext.Set<ProductType>();

        public async Task<PagedList<Product>> ProductFilterPrice(ProductParameters productParameters)
        {
          var products = await FindByCondition(p=>(p.Price <= productParameters.maxPrice && p.Price >= productParameters.minPrice) ,false).ToListAsync();
          return PagedList<Product>.ToPagedList(products,productParameters.PageNumber,productParameters.PageSize);
        }

        public async Task<PagedList<Product>> ProductSearch(ProductParameters productParameters)
        {
         
         if (string.IsNullOrWhiteSpace(productParameters.search))
         {
          var product = await FindAll(false).ToListAsync();
          return PagedList<Product>.ToPagedList(product,productParameters.PageNumber,productParameters.PageSize);
            
         } 
         var searchTirm = productParameters.search.Trim().ToLower();
         var productSearchResult = await FindByCondition(p=>p.Name.ToLower().Contains(searchTirm),false).sort(productParameters.orderBy).ToListAsync();
          return PagedList<Product>.ToPagedList(productSearchResult,productParameters.PageNumber,productParameters.PageSize);
        }
    }
    
}