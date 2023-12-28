using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.DTO;
using WebAPI.Models;
using System.Linq.Dynamic.Core;
using Microsoft.AspNetCore.Mvc.Routing;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authorization;

    [ApiController]
    [Route("[controller]/[action]")]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        
       [HttpGet(Name = "Book")]
        [ResponseCache(Location = ResponseCacheLocation.Any, Duration = 60)]
        public async Task<IActionResult> Get()
        {
            // Bắt đầu với câu truy vấn cơ bản
            var query = _context.Books
                .Include(b => b.Author)
                .Include(b => b.Category);
            

            // Thực hiện câu truy vấn và tạo kết quả
            var result = new RestDTO<Book[]>
            {
                Data = await query.ToArrayAsync(),
                Links = new List<LinkDTO>
                {
                    new LinkDTO(Url.Action(null, "Books", null, Request.Scheme)!,"self","GET")
                }
            };

            return Ok(result);
        }

        //Read (Lấy thông tin sách):
        [HttpGet("{id}", Name = "BookId")]
        public async Task<IActionResult> GetBookById(int id)
        {
            var book = await _context.Books
                .Include(b => b.Author)
                .Include(b => b.Category)
                .FirstOrDefaultAsync(b => b.BookID == id);

            if (book == null)
            {
                return NotFound();
            }

            return Ok(book);
        }
    }
