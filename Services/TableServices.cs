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


}
