using Restaurant.Enums;

public class ProductGetResponse
{
    public required string Id { get; set; }

    public required string Name { get; set; }

    public required decimal Price { get; set; }

    public required string Description { get; set; }

    public required CategoryEnum Category { get; set; }
}
