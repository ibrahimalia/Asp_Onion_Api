using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class BacksetController : ApiBaseController
    {
        private readonly IBacksetRepository _backsetRepository;
        public BacksetController(IBacksetRepository backsetRepository)
        {
            _backsetRepository = backsetRepository;
        }
        [HttpGet]
        public async Task<ActionResult<CustomerBackset>> GetBacKsetById(string id){

            var results = await _backsetRepository.GetBacksetAsync(id);
            return Ok(results ?? new CustomerBackset(id));
            
        }
        [HttpPost]
        public async Task<ActionResult<CustomerBackset>> SetBacKsetById(CustomerBackset customerBackset){

            var results = await _backsetRepository.UpdateBacksetAsync(customerBackset);
            return Ok(results);
            
        }
        [HttpDelete]
        public async Task<ActionResult<bool>> DeleteBacKsetById(string id){

            var results = await _backsetRepository.DeleteBacksetAsync(id);
            return Ok(results);
            
        }
    }
}