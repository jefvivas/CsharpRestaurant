using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
public class OrderLog
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    [BsonElement("OrderedItems")]
    public List<OrderItem> OrderedItems { get; set; }

    [BsonElement("Date")]
    public DateTime Date { get; set; }

    [BsonElement("Total")]

    public double Total { get; set; }

    public OrderLog(double Total)
    {
        this.Id = ObjectId.GenerateNewId().ToString();
        this.OrderedItems = new List<OrderItem>();
        this.Total = Total;
        this.Date = DateTime.UtcNow;

    }
}

