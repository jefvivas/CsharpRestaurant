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
        return await _collection.Find(p => p.Number == number).FirstOrDefaultAsync();
    }

    public async Task CreateTable(Table table)
    {
        await _collection.InsertOneAsync(table);
    }
    public async Task InsertProductsIntoTable(Table table, ConsumePostBody consumeBody)
    {
        foreach (var item in consumeBody.Items)
        {
            if (!table.ConsumedProducts.ContainsKey(item.ProductId))

            {
                table.ConsumedProducts.Add(item.ProductId, item.Quantity);

            }
            else
            {
                table.ConsumedProducts[item.ProductId] += item.Quantity;
            }


        }

        var filter = Builders<Table>.Filter.Eq(t => t.Number, table.Number);
        var update = Builders<Table>.Update
            .Set(t => t.ConsumedProducts, table.ConsumedProducts);

        await _collection.ReplaceOneAsync(filter, table);

    }



}
