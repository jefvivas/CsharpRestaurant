using MongoDB.Driver;
using Restaurant.Interfaces;

namespace Restaurant.Services;

public class OrderLogServices : IOrderLogServices
{
    private readonly IMongoCollection<OrderLog> _collection;

    public OrderLogServices(IMongoCollection<OrderLog> collection)
    {
        _collection = collection;
    }


    public async Task CreateLog(OrderLog log)
    {
        await _collection.InsertOneAsync(log);
    }




}
