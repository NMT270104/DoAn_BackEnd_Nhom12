using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.DTO;
using WebAPI.Models;
using System.Linq.Dynamic.Core;
using Microsoft.AspNetCore.Mvc.Routing;
using System.Text.Json;
using System.Text.Json.Serialization;   

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

        //Create (Thêm mới sách):
        [HttpPost(Name = "CreateBook")]
        public async Task<IActionResult> Create([FromBody] BookDTO bookDTO)
        {
            if (ModelState.IsValid)
            {
                // Check if AuthorID and CategoryID are valid integers
                if (!int.TryParse(bookDTO.AuthorID.ToString(), out int authorId) || !int.TryParse(bookDTO.CategoryID.ToString(), out int categoryId))
                {
                    return BadRequest("Invalid AuthorID or CategoryID");
                }

                // Map dữ liệu từ BookDTO vào Book
                var book = new Book
                {
                    NameBook = bookDTO.NameBook,
                    AuthorID = authorId,
                    CategoryID = categoryId,
                    Image = bookDTO.Image,
                    Description = bookDTO.Description,
                    Price = bookDTO.Price,
                    Quantity = bookDTO.Quantity
                };

                // Thêm mới Book
                _context.Books.Add(book);

                await _context.SaveChangesAsync();

                // Lấy BookID sau khi đã thêm vào cơ sở dữ liệu
                int bookId = book.BookID;

                // Trả về thông tin chi tiết của sách đã thêm mới
                var createdBook = await _context.Books
                    .Include(b => b.Author)
                    .Include(b => b.Category)
                    .FirstOrDefaultAsync(b => b.BookID == bookId);

                return CreatedAtAction("GetBooks", new { id = createdBook.BookID }, createdBook);
            }

            return BadRequest(ModelState);
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


        // Update (Sửa sách)
        [HttpPut("{id}", Name = "UpdateBook")]
        public async Task<IActionResult> Update(int id, [FromBody] BookDTO bookDTO)
        {
            if (ModelState.IsValid)
            {
                // Check if AuthorID and CategoryID are valid integers
                if (!int.TryParse(bookDTO.AuthorID.ToString(), out int authorId) || !int.TryParse(bookDTO.CategoryID.ToString(), out int categoryId))
                {
                    return BadRequest("Invalid AuthorID or CategoryID");
                }

                // Lấy sách cần sửa từ cơ sở dữ liệu
                var existingBook = await _context.Books.FindAsync(id);

                if (existingBook == null)
                {
                    return NotFound(); // Trả về 404 nếu không tìm thấy sách
                }

                // Cập nhật thông tin sách
                existingBook.NameBook = bookDTO.NameBook;
                existingBook.AuthorID = authorId;
                existingBook.CategoryID = categoryId;
                existingBook.Image = bookDTO.Image;
                existingBook.Description = bookDTO.Description;
                existingBook.Price = bookDTO.Price;
                existingBook.Quantity = bookDTO.Quantity;

                // Lưu thay đổi vào cơ sở dữ liệu
                await _context.SaveChangesAsync();

                // Trả về thông tin sách sau khi sửa
                var updatedBook = await _context.Books
                    .Include(b => b.Author)
                    .Include(b => b.Category)
                    .FirstOrDefaultAsync(b => b.BookID == id);

                return Ok(updatedBook);
            }

            return BadRequest(ModelState);
        }

        

        //Delete (Xóa sách):
        [HttpDelete("{id}", Name = "DeleteBook")]
        public async Task<IActionResult> Delete(int id)
        {
            var book = await _context.Books.FindAsync(id);

            if (book == null)
            {
                return NotFound();
            }

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
                string deleted = "Đã xoá sách này!";
            return Ok(deleted);
        }


}
