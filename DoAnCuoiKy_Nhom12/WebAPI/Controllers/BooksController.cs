using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.DTO;
using WebAPI.Models;

namespace WebAPI.Controllers{
    
    [ApiController]
    [Route("[controller]")]
    public class BooksController : ControllerBase{
        private readonly ApplicationDbContext _context;
        private readonly ILogger<BooksController> _logger;

        public BooksController(ApplicationDbContext context ,ILogger<BooksController> logger){
            _context = context;
            _logger = logger;
        }
        [HttpGet(Name = "GetBooks")]
        [ResponseCache(Location = ResponseCacheLocation.Any, Duration = 60)]
        public async Task<RestDTO<Book[]>> Get(){
            var query = _context.Books;
            return new RestDTO<Book[]>(){
                Data = await query.ToArrayAsync(),
                Links = new List<LinkDTO>{
                    new LinkDTO(Url.Action(null, "Books", null, Request.Scheme)!,"self","GET")
                }
                //new Book() { BookID = 1, NameBook = "Đắc Nhân Tâm", Price = 2000000, Quantity =100 },

            };
        }
    }
}