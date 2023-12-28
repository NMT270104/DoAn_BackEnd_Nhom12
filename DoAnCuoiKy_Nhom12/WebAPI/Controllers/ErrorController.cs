using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace WebAPI.Controllers
{
    [ApiController]
    [Route("/error")]
    [ApiExplorerSettings(IgnoreApi = true)] // Exclude from Swagger
    public class ErrorController : ControllerBase
    {
        [HttpGet("{statusCode}")]
        public IActionResult HandleErrorCode(int statusCode)
        {
            // Handle different status codes here, e.g., return specific JSON for 404

            if (statusCode == 404)
            {
                return NotFound(/*"Resource not found."*/);
            }

            return Problem(); // generic error
        }
    }

}