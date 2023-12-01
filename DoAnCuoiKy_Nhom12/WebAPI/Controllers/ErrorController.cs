using Microsoft.AspNetCore.Mvc;
namespace WebAPI.Controllers
{
    [ApiController]
    [Route("/error")] // Đặt route chung cho tất cả các hành động trong controller
    public class ErrorController: Controller{
        
        [HttpGet("test")] // Đặt route cụ thể cho hành động Test
        public IActionResult Test(){
            throw new Exception("test");
        }

    }
}