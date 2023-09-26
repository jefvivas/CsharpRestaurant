namespace Restaurant.Models;

public class TableProductItem
{
    public required string ProductId { get; set; }
    public int Quantity { get; set; } = 1;

    public required string ProductName { get; set; }
}