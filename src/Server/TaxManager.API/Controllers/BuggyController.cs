using Microsoft.AspNetCore.Mvc;
using TaxManager.API.Errors;
using TaxManager.Infra.Data;

namespace TaxManager.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class BuggyController : ControllerBase
    {
        private readonly TaxManagerContext _context;

        public BuggyController(TaxManagerContext context)
        {
            _context = context;
        }

        [HttpGet("notfound")]
        public ActionResult GetNotFoundResult()
        {
            var tmp = _context.Municipalities.Find(99999);

            if (tmp == null)
            {
                return NotFound(new ApiResponse(404));
            }
            
            return Ok();
        }

        [HttpGet("servererror")]
        public ActionResult GetServerErrorResult()
        {
            var tmp = _context.Municipalities.Find(99999);

            var tmpToReturn = tmp.ToString();

            return Ok();
        }

        [HttpGet("badrequest")]
        public ActionResult GetBadRequestResult()
        {
            return BadRequest(new ApiResponse(400));
        }

        [HttpGet("badrequest/{id}")]
        public ActionResult GetBadValidationResult(int id)
        {
            return Ok();
        }
    }
}