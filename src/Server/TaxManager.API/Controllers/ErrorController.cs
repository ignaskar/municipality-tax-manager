using Microsoft.AspNetCore.Mvc;
using TaxManager.API.Errors;

namespace TaxManager.API.Controllers
{
    [ApiController]
    [Route("api/v1/errors/{statusCode}")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorController : ControllerBase
    {
        public IActionResult Error(int statusCode)
        {
            return new ObjectResult(new ApiResponse(statusCode));
        }
    }
}