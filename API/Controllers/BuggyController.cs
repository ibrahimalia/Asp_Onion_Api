using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class BuggyController : ApiBaseController
    {
        private readonly StoreContext _storeContext;
        public BuggyController(StoreContext storeContext)
        {
            _storeContext = storeContext;
        }

        [HttpGet("notFound")]
        public ActionResult GetNotFoundRequest()
        {
            var thing = _storeContext.Products.Find(2000);
            if (thing == null)
            {
                return NotFound();
            }
            return Ok();
        }

        [HttpGet("serverError")]
        public ActionResult GetServerErrorRequest()
        {
            var thing = _storeContext.Products.Find(2000);
            var things = thing.ToString();
            return Ok();
        }

        [HttpGet("badRequest")]
        public ActionResult GetBadRequest()
        {

            return BadRequest();
        }

        [HttpGet("badRequest/{id}")]
        public ActionResult GetNotFoundRequest(int id)
        {

            return Ok();
        }

    }
}