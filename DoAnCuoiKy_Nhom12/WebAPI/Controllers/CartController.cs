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
public class CartController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly List<ShoppingCartItem> ShoppingCart;

    public CartController(ApplicationDbContext context, List<ShoppingCartItem> shoppingCart)
    {
        _context = context;
        ShoppingCart = shoppingCart;
    }

    [HttpPost("{id}/AddToCart", Name = "AddToCart")]
    public IActionResult AddToCart(int id, [FromBody] int quantity)
    {
        var book = _context.Books.Find(id);

        if (book == null)
        {
            return NotFound("Sản phẩm không tồn tại.");
        }

        var existingItem = ShoppingCart.FirstOrDefault(item => item.BookId == id);

        if (existingItem != null)
        {
            existingItem.Quantity += quantity;
        }
        else
        {
            ShoppingCart.Add(new ShoppingCartItem { 
                BookId = id, 
                Quantity = quantity, 
                Book = book 
            });
        }

        return Ok(ShoppingCart);
    }

    [HttpGet(Name = "ViewCart")]
    public IActionResult ViewCart()
    {
        return Ok(new { ShoppingCart });
    }

}

