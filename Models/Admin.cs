using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Restaurant.Models;

public class Admin
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }

    public Admin(string username, string password)
    {
        Username = username;
        Password = password;
    }
}

