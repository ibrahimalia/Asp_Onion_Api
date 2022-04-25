using System.Threading.Tasks;
using Core.Entities;

namespace Infrastructure.Data.SeedData.Factories
{
    public class ProductTypeFactory : BaseFactory<ProductType>
    {
        public ProductTypeFactory(StoreContext storeContext) : base(storeContext)
        {
        }

        public override async Task<ProductType> Make()=> new ProductType()
        {
            Name = Faker.Name.First(),
        };
    }
}