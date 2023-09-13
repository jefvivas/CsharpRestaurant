using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace Restaurant.Models;

public class Table
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    public string Number { get; set; }
    public string Password { get; set; }

    public Table(string number, string password)
    {
        Number = number;
        Password = password;
    }
}

