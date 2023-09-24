namespace Restaurant.Models;

public class TableAllItemsModel
{
    public List<ProductItem> Items { get; set; }

    public TableAllItemsModel()
    {
        Items = new List<ProductItem>();
    }
}
