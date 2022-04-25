using System.Threading.Tasks;
using Infrastructure.Data.SeedData.Factories;

namespace Infrastructure.Data.SeedData
{
    public class Seed
    {
        public static async Task SeedData(StoreContext storeContext){
            await new ProductBrandFactory(storeContext).Count(1).Create();
            await new ProductTypeFactory(storeContext).Count(1).Create();
            await new ProductFactory(storeContext).Count(1).Create();
        }
    }
}