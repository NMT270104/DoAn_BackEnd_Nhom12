using WebAPI.Models;

public class ShoppingCartItem
{
    public int BookId { get; set; }
    public string BookName { get; set; }
    public double Price { get; set; }
    public int Quantity { get; set; }
    public double TotalPrice => Price * Quantity; // Thêm thuộc tính TotalPrice

    public Book Book { get; internal set; }
}
