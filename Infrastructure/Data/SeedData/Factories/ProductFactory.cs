using System;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.SeedData.Factories
{
    public class ProductFactory : BaseFactory<Product>
    {
        public ProductFactory(StoreContext storeContext) : base(storeContext)
        {
        }

        public override async Task<Product> Make()
        {
            var random = new Random();
            var productBrandIds = await _storeContext.ProductBrands.Select(pd=> pd.Id).ToListAsync();
            var productTypeIds = await _storeContext.ProductTypes.Select(pd=> pd.Id).ToListAsync();
            return new Product()
                    {
                        Name= Faker.Name.First(),
                        Description=Faker.Lorem.Sentence(6),
                        Price=Faker.RandomNumber.Next(1000, 10000),
                        PictureUrl=Faker.Internet.DomainName(),
                        ProductTypeId=productTypeIds.ElementAt(random.Next(productTypeIds.Count - 1)),
                        ProductBrandId=productBrandIds.ElementAt(random.Next(productBrandIds.Count - 1))
                    };
        }
    }
}