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
public class AuthorsController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public AuthorsController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet(Name = "GetAuthors")]
    public async Task<IActionResult> GetAuthors()
    {
        var authors = await _context.Authors.ToListAsync();
        return Ok(authors);
    }

    [HttpGet("{id}", Name = "GetAuthorById")]
    public async Task<IActionResult> GetAuthorById(int id)
    {
        var author = await _context.Authors.FindAsync(id);
        if (author == null)
        {
            return NotFound();
        }
        return Ok(author);
    }

    [HttpPost(Name = "CreateAuthor")]
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

                return CreatedAtAction("GetBooks", new { id = authorMap.AuthorID }, authorMap);
        }
        return BadRequest(ModelState);
    }

    [HttpPut("{id}", Name = "UpdateAuthor")]
    public async Task<IActionResult> UpdateAuthor(int id, [FromBody] AuthorDTO updatedAuthor)
    {
        // if (id != updatedAuthor.AuthorID)
        // {
        //     return BadRequest("Invalid ID");
        // }

        var existingAuthor = await _context.Authors.FindAsync(id);
        if (existingAuthor == null)
        {
            return NotFound("Author not found");
        }

        existingAuthor.AuthorName = updatedAuthor.AuthorName;

        await _context.SaveChangesAsync();
        // Trả về thông tin tác giả sau khi sửa
        var updatedauthor = await _context.Authors
            .ToDynamicArrayAsync();
        return Ok(updatedauthor);
    }

    [HttpDelete("{id}", Name = "DeleteAuthor")]
    public async Task<IActionResult> DeleteAuthor(int id)
    {
        var author = await _context.Authors.FindAsync(id);
        if (author == null)
        {
            return NotFound();
        }

        _context.Authors.Remove(author);
        await _context.SaveChangesAsync();
        string deleted = "tác giả này đã được xoá!";

        return Ok(deleted);
    }
}

