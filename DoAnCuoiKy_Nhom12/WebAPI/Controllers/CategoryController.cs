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
public class CategoriesController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public CategoriesController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet(Name = "GetCategories")]
    public async Task<IActionResult> GetCategories()
    {
        var categories = await _context.Categories.ToListAsync();
        return Ok(categories);
    }

    [HttpGet("{id}", Name = "GetCategoryById")]
    public async Task<IActionResult> GetCategoryById(int id)
    {
        var category = await _context.Categories.FindAsync(id);
        if (category == null)
        {
            return NotFound();
        }
        return Ok(category);
    }

    [HttpPost(Name = "CreateCategory")]
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

                return CreatedAtAction("GetBooks", new { id = categoryMap.CategoryID }, categoryMap);
        }
        return BadRequest(ModelState);
    }

    [HttpPut("{id}", Name = "UpdateCategory")]
    public async Task<IActionResult> UpdateCategory(int id, [FromBody] CategoryDTO updatedCategory)
    {
        // if (id != updatedCategory.CategoryID)
        // {
        //     return BadRequest("Invalid ID");
        // }

        var existingCategory = await _context.Categories.FindAsync(id);
        if (existingCategory == null)
        {
            return NotFound("Category not found");
        }

        existingCategory.CategoryName = updatedCategory.CategoryName;

        await _context.SaveChangesAsync();
        // Trả về thông tin thể loại sau khi sửa
        var updatedcategory = await _context.Categories
            .ToDynamicArrayAsync();
        return Ok(updatedcategory);
    }

    [HttpDelete("{id}", Name = "DeleteCategory")]
    public async Task<IActionResult> DeleteCategory(int id)
    {
        var category = await _context.Categories.FindAsync(id);
        if (category == null)
        {
            return NotFound();
        }

        _context.Categories.Remove(category);
        await _context.SaveChangesAsync();
        string deleted = "Thể loại này đã được xoá!";

        return Ok(deleted);
    }
}

