using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class Product
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    [BsonElement("Name")]
    public string Name { get; set; }

    [BsonElement("Price")]
    public decimal Price { get; set; }

    [BsonElement("Available")]
    public bool isAvailable { get; set; }

    public Product(string Name, decimal Price, bool isAvailable = true)
    {
        this.Name = Name;
        this.Price = Price;
        this.isAvailable = isAvailable;
    }

}

