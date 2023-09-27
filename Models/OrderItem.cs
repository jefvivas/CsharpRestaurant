using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class OrderItem
{
    [BsonElement("productId")]
    public required string ProductId { get; set; }

    [BsonElement("quantity")]
    public int Quantity { get; set; }
}

