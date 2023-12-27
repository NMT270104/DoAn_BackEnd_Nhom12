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
    [Authorize(Roles = "Administrator")]
    public class AdminController : ControllerBase{
        private readonly ApplicationDbContext _context;
        private readonly ILogger<AdminController> _logger;

        public AdminController(ApplicationDbContext context ,ILogger<AdminController> logger){
            _context = context;
            _logger = logger;
        }

        [HttpGet(Name = "AdminGetAuthors")]
        public async Task<IActionResult> GetAuthors()
        {
            var authors = await _context.Authors.ToListAsync();
            return Ok(authors);
        }

        [HttpGet("{id}", Name = "AdminGetAuthorById")]
        public async Task<IActionResult> GetAuthorById(int id)
        {
            var author = await _context.Authors.FindAsync(id);
            if (author == null)
            {
                return NotFound("Không tìm thấy tác giả này.");
            }
            return Ok(author);
        }

        [HttpPost(Name = "AdminCreateAuthor")]
        public async Task<IActionResult> CreateAuthor([FromBody] AuthorDTO authorDTO)
        {
        // Map dữ liệu từ AuthorDTO vào Author
            var authorMap = new Author
            {
                AuthorName = authorDTO.AuthorName
            };
            if (ModelState.IsValid)
            {
                _context.Authors.Add(authorMap);
                await _context.SaveChangesAsync();
                // Trả về thông tin chi tiết của tác giả đã thêm mới
                var createdAuthor = await _context.Authors
                        .ToDynamicArrayAsync();

                    return CreatedAtAction("CreateAuthor", new { id = authorMap.AuthorID }, authorMap);
            }
            return BadRequest(ModelState);
        }

        [HttpPut("{id}", Name = "AdminUpdateAuthor")]
        public async Task<IActionResult> UpdateAuthor(int id, [FromBody] AuthorDTO updatedAuthor)
        {
            // if (id != updatedAuthor.AuthorID)
            // {
            //     return BadRequest("Invalid ID");
            // }

            var existingAuthor = await _context.Authors.FindAsync(id);
            if (existingAuthor == null)
            {
                return NotFound("Không tìm thấy tác giả này.");
            }

            existingAuthor.AuthorName = updatedAuthor.AuthorName;

            await _context.SaveChangesAsync();
            // Trả về thông tin tác giả sau khi sửa
            var updatedauthor = await _context.Authors
                .ToDynamicArrayAsync();
            return Ok(updatedauthor);
        }

        [HttpDelete("{id}", Name = "AdminDeleteAuthor")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            var author = await _context.Authors.FindAsync(id);
            if (author == null)
            {
                return NotFound("Không tìm thấy tác giả này.");
            }

            _context.Authors.Remove(author);
            await _context.SaveChangesAsync();
            string deleted = "tác giả này đã được xoá!";

            return Ok(deleted);
        }

        [HttpGet(Name = "AdminGetCategories")]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _context.Categories.ToListAsync();
            return Ok(categories);
        }

        [HttpGet("{id}", Name = "AdminGetCategoryById")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound("Không tìm thấy thể loại này.");
            }
            return Ok(category);
        }
        
        [HttpPost(Name = "AdminCreateCategory")]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryDTO categoryDTO)
        {
            // Map dữ liệu từ CategoryDTO vào Category
            var categoryMap = new Category
            {
                CategoryName = categoryDTO.CategoryName
            };
            if (ModelState.IsValid)
            {
                _context.Categories.Add(categoryMap);
                await _context.SaveChangesAsync();
                // Trả về thông tin chi tiết của thể loại đã thêm mới
                var createdCategory = await _context.Categories
                        .ToDynamicArrayAsync();

                    return CreatedAtAction("CreateCategory", new { id = categoryMap.CategoryID }, categoryMap);
            }
            return BadRequest(ModelState);
        }

        [HttpPut("{id}", Name = "AdminUpdateCategory")]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] CategoryDTO updatedCategory)
        {
            // if (id != updatedCategory.CategoryID)
            // {
            //     return BadRequest("Invalid ID");
            // }

            var existingCategory = await _context.Categories.FindAsync(id);
            if (existingCategory == null)
            {
                return NotFound("Không tìm thấy thể loại này.");
            }

            existingCategory.CategoryName = updatedCategory.CategoryName;

            await _context.SaveChangesAsync();
            // Trả về thông tin thể loại sau khi sửa
            var updatedcategory = await _context.Categories
                .ToDynamicArrayAsync();
            return Ok(updatedcategory);
        }

        [HttpDelete("{id}", Name = "AdminDeleteCategory")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound("Không tìm thấy thể loại này.");
            }

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            string deleted = "Thể loại này đã được xoá!";

            return Ok(deleted);
        }


        // lấy toàn bộ sách
        [HttpGet(Name = "AdminGetBooks")]
        [ResponseCache(Location = ResponseCacheLocation.Any, Duration = 60)]
        public async Task<IActionResult> GetBook(
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
        [HttpPost(Name = "AdminCreateBook")]
        public async Task<IActionResult> CreateBook([FromBody] BookDTO bookDTO)
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

                return CreatedAtAction("CreateBook", new { id = createdBook.BookID }, createdBook);
            }

            return BadRequest(ModelState);
        }


        //Read (Lấy thông tin sách):
        [HttpGet("{id}", Name = "AdminGetBookById")]
        public async Task<IActionResult> GetBookById(int id)
        {
            var book = await _context.Books
                .Include(b => b.Author)
                .Include(b => b.Category)
                .FirstOrDefaultAsync(b => b.BookID == id);

            if (book == null)
            {
                return NotFound("Không tìm thấy sách này.");
            }

            return Ok(book);
        }

        // Update (Sửa sách)
        [HttpPut("{id}", Name = "AdminUpdateBook")]
        public async Task<IActionResult> UpdateBook(int id, [FromBody] BookDTO bookDTO)
        {
            if (ModelState.IsValid)
            {
                // Check if AuthorID and CategoryID are valid integers
                if (!int.TryParse(bookDTO.AuthorID.ToString(), out int authorId) || !int.TryParse(bookDTO.CategoryID.ToString(), out int categoryId))
                {
                    return BadRequest("ID tác giả hoặc ID thể loại không hợp lệ.");
                }

                // Lấy sách cần sửa từ cơ sở dữ liệu
                var existingBook = await _context.Books.FindAsync(id);

                if (existingBook == null)
                {
                    return NotFound("Không tìm thấy sách này."); // Trả về 404 nếu không tìm thấy sách
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
        [HttpDelete("{id}", Name = "AdminDeleteBook")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var book = await _context.Books.FindAsync(id);

            if (book == null)
            {
                return NotFound("Không tìm thấy sách này.");
            }

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
                string deleted = "Đã xoá sách này!";
            return Ok(deleted);
        }
        

}
