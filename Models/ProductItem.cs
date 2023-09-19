namespace Restaurant.Models;

public class ProductItem
{
    public required string ProductId { get; set; }
    public int Quantity { get; set; } = 1;
}
