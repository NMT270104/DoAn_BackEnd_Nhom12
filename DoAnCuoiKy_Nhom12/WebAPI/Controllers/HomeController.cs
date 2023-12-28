using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.DTO;
using WebAPI.Models;
using System.Linq.Dynamic.Core;
using Microsoft.AspNetCore.Mvc.Routing;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;

[ApiController]
    [Route("[controller]/[action]")]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public HomeController(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
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



    // Key lưu chuỗi json của Cart
    public const string CARTKEY = "cart";

    // Lấy cart từ Session (danh sách CartItem)
    List<ShoppingCartItem> GetCartItems() {
    var session = _httpContextAccessor.HttpContext.Session;
    string jsoncart = session.GetString(CARTKEY);
    
    if (jsoncart != null) {
        try {
            return JsonConvert.DeserializeObject<List<ShoppingCartItem>>(jsoncart);
        } catch (Newtonsoft.Json.JsonException ex) {
            // Ghi log hoặc xử lý ngoại lệ theo cách cần thiết
            Console.Error.WriteLine($"Lỗi giải mã dữ liệu giỏ hàng: {ex.Message}");
        }
    }

    return new List<ShoppingCartItem>();
    }


    // Xóa cart khỏi session
    void ClearCart () {
        var session = _httpContextAccessor.HttpContext.Session;
        session.Remove (CARTKEY);
    }

    // Lưu Cart (Danh sách CartItem) vào session
    void SaveCartSession (List<ShoppingCartItem> ls) {
        var session = _httpContextAccessor.HttpContext.Session;
        string jsoncart = JsonConvert.SerializeObject (ls);
        session.SetString (CARTKEY, jsoncart);
    }

        /// Thêm sản phẩm vào cart
   [HttpPost("{bookid}", Name = "AddToCart")]
    public IActionResult AddToCart([FromRoute]int bookid)
    {

        var book = _context.Books
            .Where(p => p.BookID == bookid)
            .FirstOrDefault();
        if (book == null)
            return NotFound("Không có sản phẩm");

        // Xử lý đưa vào Cart ...
        var cart = GetCartItems ();
        var cartitem = cart.Find (p => p.Book.BookID == bookid);
        if (cartitem != null) {
        // Đã tồn tại, tăng thêm 1
        cartitem.Quantity++;
    } else {
        //  Thêm mới
        cart.Add (new ShoppingCartItem () { Quantity = 1, Book = book });
    }

        return RedirectToAction(nameof(Cart));
    }


    /// xóa item trong cart
    [HttpDelete("/removecart/{bookid:int}", Name = "removecart")]
    public IActionResult RemoveCart([FromRoute]int bookid) {
        // Xử lý xóa một mục của Cart ...
        var cart = GetCartItems ();
        var cartitem = cart.Find (p => p.Book.BookID == bookid);
        if (cartitem != null) {
            // Đã tồn tại, tăng thêm 1
            cart.Remove(cartitem);
        }

    SaveCartSession (cart);
        return RedirectToAction(nameof(Cart));
    }

    /// Cập nhật
    [HttpPost ("/updatecart", Name = "updatecart")]
    public IActionResult UpdateCart([FromForm]int bookid, [FromForm]int quantity) {
        // Cập nhật Cart thay đổi số lượng quantity ...
        var cart = GetCartItems ();
        var cartitem = cart.Find (p => p.Book.BookID == bookid);
        if (cartitem != null) {
            // Đã tồn tại, tăng thêm 1
            cartitem.Quantity = quantity;
        }
        SaveCartSession (cart);
        // Trả về mã thành công (không có nội dung gì - chỉ để Ajax gọi)
        return Ok();

    }


    // Hiện thị giỏ hàng
    [HttpGet("/cart", Name = "cart")]
    public IActionResult Cart()
    {
        var cartItems = GetCartItems();
        Console.WriteLine($"Number of items in cart: {cartItems.Count}");
        return Ok(cartItems);
    }


    [HttpPost("/checkout")]
    public IActionResult CheckOut()
    {
        // Xử lý khi đặt hàng
        return Ok();
    }

}
