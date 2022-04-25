using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using API.Controllers;
using Core.Entities;
using Core.Interfaces.IRepository;
using FluentAssertions;
using Infrastructure.Data;
using Infrastructure.Data.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace ApiTest
{
    public class UnitTest1
    {
        // private readonly Mock<IUnitOfWork> unitOfWork = new();
        [Fact]
        public async Task Test1()
        {
             var unitOfWork = new Mock<IUnitOfWork>() ;
            //  unitOfWork.Setup(u=>u.Product.FindByConditionD(p=>p.Id == 10000 , false)).Returns(Task.FromResult(new Product()));
            unitOfWork.Setup(u => u.Product.FindByConditionD(p => p.Id == 10000, false)).ReturnsAsync(new Product());
            var controller = new ProductController(unitOfWork.Object);
            var results = await  controller.getProductId(10000);
            Assert.IsType<ActionResult<Product>>(results);
        }
        [Fact]
        public async void Test2(){
            var product = new Product(){
                Id = 2 , 
                Name = "sdsd",
                Description="vb",
                Price=2,
                PictureUrl="jkjkjk",
                ProductTypeId =1,
                ProductBrandId =1
            };
            var unitOfWork = new Mock<IUnitOfWork>() ;
            var repo = new Mock<IProductRepository>();
            repo.Setup(r=>r.FindByConditionD(p=>p.Id == 1000 ,false)).ReturnsAsync(product);
            unitOfWork.Setup(u=>u.Product).Returns(repo.Object);
            var controller = new ProductController(unitOfWork.Object);
            var results =await  controller.getProductId(1000);
            // var res = (Product) ((ObjectResult)results.Result).Value;
            Assert.Equal(results.Value.Id,product.Id);
            
        }
        

    }
}
