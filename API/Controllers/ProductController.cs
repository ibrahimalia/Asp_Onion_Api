using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using API.Errors;
using AutoMapper;
using Core.Entities;
using Core.Interfaces.IRepository;
using Core.pagination;
using Infrastructure.Data;
using Infrastructure.Data.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace API.Controllers
{

    public class ProductController : ApiBaseController
    {
        private readonly IUnitOfWork _unitOfwork;
        private readonly IMemoryCache _memoryCache;//use to cache in-memory

        public ProductController(IUnitOfWork unitOfWork, IMemoryCache memoryCache)
        {
            _unitOfwork = unitOfWork;
            _memoryCache = memoryCache;
        }
        // [HttpGet]
        // public async Task<ActionResult<List<Product>>> getProduct()
        // {
        //     var products = await _unitOfwork.Product.FindAll(false).ToListAsync();
        //     var results = _mapper.Map<List<ProductDTO>>(products);
        //     return Ok(results);
        // }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> getProductId(int id)
        {

            var product = await _unitOfwork.Product.FindByConditionD(p => p.Id == id, false);
            return new ActionResult<Product>(product);

        }
        //apply pagination
        [HttpGet("brands")]
        public async Task<ActionResult<List<ProductBrand>>> getProductBrand([FromQuery] ProductParameters productParameters)
        {
            var productBrands = await _unitOfwork.Product.getProductBrands(productParameters, false).ToListAsync();
            var results = PagedList<ProductBrand>.ToPagedList(productBrands, productParameters.PageNumber, productParameters.PageSize);
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(results.MetaData));
            return Ok(new ApiResponse(200, results));
        }

        [HttpGet("types")]
        public async Task<ActionResult<List<ProductType>>> getProductType()
        {
            var cacheKey = "types";
            if (!_memoryCache.TryGetValue(cacheKey, out List<ProductType> ProductTypes))
            {
                ProductTypes = await _unitOfwork.Product.getProductTypes(false).ToListAsync();
                var cacheExpiryOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpiration = DateTime.Now.AddSeconds(50),
                    Priority = CacheItemPriority.High,
                    SlidingExpiration = TimeSpan.FromSeconds(20)
                };
                _memoryCache.Set(cacheKey, ProductTypes, cacheExpiryOptions);


            }
            return Ok(ProductTypes);
        }
        [HttpPost("brand")]
        public async Task<ActionResult<ProductBrand>> getProductBrandId([FromBody] ProductBrandDTO productBrandDTO)
        {
            var id = productBrandDTO.Id;
            var results = await _unitOfwork.Product.getProductBrandId(pb => pb.Id == id, false).ToListAsync();
            return Ok(results);
        }
        [HttpGet("Productfilters")]
        public async Task<ActionResult<List<Product>>> FilterProducts([FromQuery] ProductParameters productParameters)
        {
            if (!productParameters.validaion)
            {
                return BadRequest(new ApiResponse(400));
            }
            var products = await _unitOfwork.Product.ProductFilterPrice(productParameters);
            Response.Headers.Add("X-pagination", JsonConvert.SerializeObject(products.MetaData));
            return Ok(new ApiResponse(200, products));

        }
        [HttpGet("ProductSearch")]
        public async Task<ActionResult<List<Product>>> SearchProduct([FromQuery] ProductParameters productParameters)
        {
            var products = await _unitOfwork.Product.ProductSearch(productParameters);
            Response.Headers.Add("X-pagination", JsonConvert.SerializeObject(products.MetaData));
            return Ok(new ApiResponse(200, products));
        }
        //use caching in-memory


    }
}