using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class Product
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    [BsonElement("Name")]
    public string Name { get; set; }

    [BsonElement("Price")]
    public decimal Price { get; set; }

    [BsonElement("Description")]
    public string Description { get; set; }

    [BsonElement("Category")]
    public string Category { get; set; }

    [BsonElement("Available")]
    public bool IsAvailable { get; set; }

    public Product(string Name, decimal Price, string Description, string Category, bool IsAvailable = true)
    {
        this.Id = ObjectId.GenerateNewId().ToString();
        this.Name = Name;
        this.Price = Price;
        this.Description = Description;
        this.Category = Category;
        this.IsAvailable = IsAvailable;
    }

    public bool IsValid()
    {
        return !string.IsNullOrEmpty(Name) && !string.IsNullOrEmpty(Name) && !string.IsNullOrEmpty(Category) && Price > 0;
    }
}

