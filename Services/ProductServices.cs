using MongoDB.Driver;
using Restaurant.Interfaces;

namespace Restaurant.Services;

public class ProductServices : IProductServices
{
    private readonly IMongoCollection<Product> _collection;

    public ProductServices(IMongoCollection<Product> collection)
    {
        _collection = collection;
    }

    public async Task<Product> GetProductById(string id)
    {
        return await _collection.Find(p => p.Id == id).FirstOrDefaultAsync();
    }

    public async Task<Product> GetProductByName(string name)
    {
        return await _collection.Find(p => p.Name == name).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Product>> GetAllProducts()
    {
        return await _collection.Find(_ => true).ToListAsync();
    }

    public async Task<IEnumerable<Product>> GetAllAvailableProducts()
    {
        return await _collection.Find(p => p.IsAvailable == true).ToListAsync();
    }

    public async Task DeleteProduct(string id)
    {
        await _collection.DeleteOneAsync(p => p.Id == id);
    }

    public async Task CreateProduct(Product product)
    {
        await _collection.InsertOneAsync(product);
    }

    public async Task UpdateProduct(Product product)
    {
        await _collection.ReplaceOneAsync(p => p.Id == product.Id, product);

    }
}
