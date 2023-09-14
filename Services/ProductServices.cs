using MongoDB.Driver;

namespace Restaurant.Services;

public class ProductServices
{
    private readonly IMongoCollection<Product> _collection;

    public ProductServices(IMongoCollection<Product> collection)
    {
        _collection = collection;
    }

    public Product GetProductById(string id)
    {
        return _collection.Find(p => p.Id == id).FirstOrDefault();
    }

    public Product GetProductByName(string name)
    {
        return _collection.Find(p => p.Name == name).FirstOrDefault();
    }

    public IEnumerable<Product> GetAllProducts()
    {
        return _collection.Find(_ => true).ToList();
    }

    public void DeleteProduct(string id)
    {
        _collection.DeleteOne(p => p.Id == id);
    }

    public void CreateProduct(Product product)
    {
        _collection.InsertOne(product);
    }

    public void UpdateProduct(Product product)
    {
        _collection.ReplaceOne(p => p.Id == product.Id, product);

    }
}
