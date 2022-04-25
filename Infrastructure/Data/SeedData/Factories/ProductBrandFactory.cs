using System.Threading.Tasks;
using Core.Entities;

namespace Infrastructure.Data.SeedData.Factories
{
    public class ProductBrandFactory : BaseFactory<ProductBrand>
    {
        public ProductBrandFactory(StoreContext storeContext) : base(storeContext)
        {
        }

        public override async Task<ProductBrand> Make() =>  new ProductBrand()
        {
            Name = Faker.Name.First(),
        };
    }
}