using Restaurant.Models;

namespace Restaurant.Interfaces;

public interface ITableServices
{
    Table GetTableByNumber(string number);
    void CreateTable(Table table);
    public void InsertProductsIntoTable(Table table, ConsumePostBody consumeBody);

}
