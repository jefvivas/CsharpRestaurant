using Restaurant.Models;

namespace Restaurant.Interfaces;

public interface ITableServices
{
    Task<Table> GetTableByNumber(string number);
    Task<Table> GetTableById(string id);
    Task CreateTable(Table table);
    Task InsertProductsIntoTable(Table table, ConsumePostBody consumeBody);

    Task UpdateTable(Table table);


}
