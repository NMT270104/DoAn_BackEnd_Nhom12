namespace WebAPI.DTO { 
public class OrderDTO
{
    public string CustomerName { get; set; }
    public string CustomerEmail { get; set; }
    public string ShippingAddress { get; set; }
    public List<OrderItemDTO> OrderItems { get; set; }
}

public class OrderItemDTO
{
    public int BookId { get; set; }
    public int Quantity { get; set; }
}
}