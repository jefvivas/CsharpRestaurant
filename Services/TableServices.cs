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


    public Table GetTableByNumber(string number)
    {
        return _collection.Find(p => p.Number == number).FirstOrDefault();
    }

    public void CreateTable(Table table)
    {
        _collection.InsertOne(table);
    }
    public void InsertProductsIntoTable(Table table, ConsumePostBody consumeBody)
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

        _collection.ReplaceOne(filter, table);

    }



}
