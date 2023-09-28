using MongoDB.Driver;
using Restaurant.Interfaces;
using Restaurant.Models;

namespace Restaurant.Services;

public class TableServices : ITableServices
{
    private readonly IMongoCollection<Table> _collection;

    public TableServices(IMongoCollection<Table> collection)
    {
        _collection = collection;
    }


    public async Task<Table> GetTableByNumber(string number)
    {
        return await _collection.Find(t => t.Number == number).FirstOrDefaultAsync();
    }

    public async Task<Table> GetTableById(string id)
    {
        return await _collection.Find(t => t.Id == id).FirstOrDefaultAsync();
    }

    public async Task CreateTable(Table table)
    {
        await _collection.InsertOneAsync(table);
    }
    public async Task InsertProductsIntoTable(Table table, ConsumePostBody consumeBody)
    {
        foreach (var item in consumeBody.Items)
        {

            var existingItem = table.ConsumedProducts.FirstOrDefault(p => p.ProductId == item.ProductId);

            if (existingItem == null)
            {
                table.ConsumedProducts.Add(new OrderItem
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity
                });
            }
            else
            {
                existingItem.Quantity += item.Quantity;
            }
        }

        var filter = Builders<Table>.Filter.Eq(t => t.Id, table.Id);
        var update = Builders<Table>.Update.Set(t => t.ConsumedProducts, table.ConsumedProducts);

        await _collection.ReplaceOneAsync(filter, table);

    }

    public async Task UpdateTable(Table table)
    {
        await _collection.ReplaceOneAsync(t => t.Id == table.Id, table);

    }



}
