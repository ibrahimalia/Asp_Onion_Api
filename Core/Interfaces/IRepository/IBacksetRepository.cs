using System.Threading.Tasks;
using Core.Entities;

namespace Core.Interfaces.IRepository
{
    public interface IBacksetRepository
    {
         Task<CustomerBackset> GetBacksetAsync(string backsetId);
         Task<CustomerBackset> UpdateBacksetAsync(CustomerBackset customerBackset);
         Task<bool> DeleteBacksetAsync(string backsetId);

    }
}