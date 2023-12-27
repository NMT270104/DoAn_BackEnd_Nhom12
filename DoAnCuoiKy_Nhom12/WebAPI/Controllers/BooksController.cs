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
    [Route("api/[controller]/[action]")]
    public class BooksController : ControllerBase{
        private readonly ApplicationDbContext _context;
        private readonly ILogger<BooksController> _logger;

        public BooksController(ApplicationDbContext context ,ILogger<BooksController> logger){
            _context = context;
            _logger = logger;
        }
        // lấy toàn bộ sách
        [HttpGet(Name = "GetBooks")]
        [ResponseCache(Location = ResponseCacheLocation.Any, Duration = 60)]
        public async Task<IActionResult> Get(
            int pageIndex = 0,
            int pageSize = 10,
            string? sortColumn = "NameBook",
            string? sortOrder = "ASC",
            string? filterAuthorName = null,
            string? filterCategoryName = null,
            string? filterNameBook = null
        )
        {
            // Bắt đầu với câu truy vấn cơ bản
            var query = _context.Books
                .Include(b => b.Author)
                .Include(b => b.Category)
                .OrderBy($"{sortColumn} {sortOrder}")
                .Skip(pageIndex * pageSize)
                .Take(pageSize);
            // Áp dụng điều kiện cho filterAuthorName nếu được chỉ định
            if (!string.IsNullOrEmpty(filterAuthorName))
            {
                query = query.Where(b => b.Author.AuthorName.Contains(filterAuthorName));
            }

            // Áp dụng điều kiện cho filterCategoryName nếu được chỉ định
            if (!string.IsNullOrEmpty(filterCategoryName))
            {
                query = query.Where(b => b.Category.CategoryName.Contains(filterCategoryName));
            }

            // Áp dụng điều kiện cho filterNameBook nếu được chỉ định
            if (!string.IsNullOrEmpty(filterNameBook))
            {
                query = query.Where(b => b.NameBook.Contains(filterNameBook));
            }

            // Thực hiện câu truy vấn và tạo kết quả
            var result = new RestDTO<Book[]>
            {
                Data = await query.ToArrayAsync(),
                PageIndex = pageIndex,
                PageSize = pageSize,
                RecordCount = await _context.Books.CountAsync(),
                Links = new List<LinkDTO>
                {
                    new LinkDTO(Url.Action(null, "Books", null, Request.Scheme)!,"self","GET")
                }
            };

            return Ok(result);
        }

        //Read (Lấy thông tin sách):
        [HttpGet("{id}", Name = "GetBookById")]
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
