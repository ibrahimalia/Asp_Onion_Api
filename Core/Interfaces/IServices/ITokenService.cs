using System.Threading.Tasks;
using Core.Entities;

namespace Core.Interfaces.IServices
{
    public interface ITokenService
    {
         public  Task<string> CreateToken(User user);
    }
}