using WebAPI.Models;

public class ShoppingCartItem
{
    public Book Book { get; set; }
    public double Price {get; set;}
    public int Quantity { get; set; }
    public double TotalPrice => Price * Quantity; // Thêm thuộc tính TotalPrice

}
