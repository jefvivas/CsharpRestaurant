using Restaurant.Models;

namespace Restaurant.Interfaces;

public interface ITableServices
{
    Task<Table> GetTableByNumber(string number);
    Task CreateTable(Table table);
    Task InsertProductsIntoTable(Table table, ConsumePostBody consumeBody);

}
