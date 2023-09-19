namespace Restaurant.Models;

public class ConsumePostBody
{
    public List<ProductItem> Items { get; set; }

    public ConsumePostBody()
    {
        Items = new List<ProductItem>();
    }
}
