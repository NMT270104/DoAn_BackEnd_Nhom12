// using Microsoft.AspNetCore.Mvc;
// using Microsoft.EntityFrameworkCore;
// using WebAPI.DTO;
// using WebAPI.Models;
// using System.Linq.Dynamic.Core;
// using Microsoft.AspNetCore.Mvc.Routing;
// using System.Text.Json;
// using System.Text.Json.Serialization;
// using Microsoft.AspNetCore.Authorization;

//     [ApiController]
//     [Route("api/[controller]/[action]")]
//     public class OrderController : Controller
// {
//     private readonly ApplicationDbContext _context;
//     private readonly List<ShoppingCartItem> ShoppingCart;

//     public OrderController(ApplicationDbContext context, List<ShoppingCartItem> shoppingCart)
//     {
//         _context = context;
//         ShoppingCart = shoppingCart;
//     }

//     [HttpPost("PlaceOrder", Name = "PlaceOrder")]
//     public IActionResult PlaceOrder([FromBody] OrderDTO orderDTO)
//     {
//         // Kiểm tra tính hợp lệ của orderDTO và thực hiện xử lý đặt hàng
//         if (orderDTO == null || orderDTO.OrderItems == null || orderDTO.OrderItems.Count == 0)
//         {
//             return BadRequest("Dữ liệu đặt hàng không hợp lệ.");
//         }

//         var order = new Order
//         {
//             CustomerName = orderDTO.CustomerName,
//             CustomerEmail = orderDTO.CustomerEmail,
//             ShippingAddress = orderDTO.ShippingAddress,
//             // Các thông tin khác của Order

//             OrderItems = orderDTO.OrderItems.Select(item => new OrderItemDTO
//             {
//                 BookId = item.BookId,
//                 Quantity = item.Quantity,
//                 // Các thông tin khác của OrderItem
//             }).ToList()
//         };

//         _context.Orders.Add(order);
//         _context.SaveChanges();

//         // Sau khi đặt hàng, xóa toàn bộ sản phẩm trong giỏ hàng
//         ShoppingCart.Clear();

//         return Ok(order);
//     }

//     [HttpGet(Name = "ViewOrder")]
//    public IActionResult ViewOrder(int orderId)
//     {
//         var order = _context.Orders
//             .Include(o => o.OrderItems)
//             .Include(oi => oi.Book)
//             .FirstOrDefault(o => o.OrderID == orderId);

//         if (order == null)
//         {
//             return NotFound("Đơn hàng không tồn tại.");
//         }

//         return Ok(order);
//     }


// }

