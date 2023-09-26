using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class ErrorLog
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    [BsonElement("ErrorType")]
    public string Type { get; set; }

    [BsonElement("ErrorMessage")]
    public string Message { get; set; }

    [BsonElement("Date")]
    public DateTime Date { get; set; }

    public ErrorLog(string Type, string Message)
    {
        this.Id = ObjectId.GenerateNewId().ToString();
        this.Type = Type;
        this.Message = Message;
        this.Date = DateTime.UtcNow;

    }
}

